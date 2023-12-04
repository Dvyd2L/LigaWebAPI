using EvaluacionDavidLlopis.DTOs;
using EvaluacionDavidLlopis.Interfaces;
using EvaluacionDavidLlopis.Models;
using EvaluacionDavidLlopis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDavidLlopis.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsuariosController(
    LigaContext DbContext,
    HashService hashService,
    TokenService tokenService
    ) : ControllerBase
{
    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] DTOUsuario input)
    {
        //// Buscamos en BD para comprovar que no exista ningun usuario ya registrado con el email introducido
        //Usuario? existeUsuario = await DbContext.Usuarios.FindAsync(input.Email);

        //if (existeUsuario is not null)
        //{
        //    return BadRequest($"El email {input.Email} ya está registrado");
        //}

        // Encriptamos la contraseña y recuperamos su hash y su salt
        IHashResult hashResult = hashService.GetHash(input.Password);

        // creamos el usuario que vamos a almacenar en BD 
        Usuario nuevoUsuario = new()
        {
            Email = input.Email,
            Password = hashResult.Hash,
            Salt = hashResult.Salt,
            Rol = input.Rol
        };

        // añadimos el nuevo usuario a BD y guardamos los cambios
        _ = await DbContext.Usuarios.AddAsync(nuevoUsuario);
        _ = await DbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] DTOUsuario input)
    {
        // Buscamos en BD para comprobar que exista un usuario registrado con el email introducido
        Usuario? usuarioBD = await DbContext.Usuarios
            .FirstOrDefaultAsync((x) => x.Email == input.Email);

        if (usuarioBD is null)
        {
            return Unauthorized($"El email {input.Email} no está registrado");
        }

        // Hasheamos el pasword proporcionado por el usuario con el salt guardado en BD
        Interfaces.IHashResult resultadoHash = hashService.GetHash(input.Password, usuarioBD.Salt);

        // Comporbamos que el resultado del hash coincida con el que ya teniamos almacenado en BD
        if (resultadoHash.Hash != usuarioBD.Password)
        {
            return Unauthorized("Credenciales incorrectas");
        }

        Interfaces.ILoginResponse loginResponse = tokenService.GenerarToken(input.Email, usuarioBD.Rol ?? "default");

        return Ok(loginResponse);
    }
}
