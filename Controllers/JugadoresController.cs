using EvaluacionDavidLlopis.DTOs;
using EvaluacionDavidLlopis.Models;
using EvaluacionDavidLlopis.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDavidLlopis.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class JugadoresController(
    LigaContext DbContext,
    SueldoService sueldoService
    ) : ControllerBase
{
    #region GET
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Jugadore>>> Get()
    {
        Jugadore[] result = await DbContext.Jugadores.ToArrayAsync();

        return Ok(result);
    }

    [HttpGet("custom")]
    public async Task<ActionResult<IEnumerable<DTOJugadoresCustom>>> GetCustom()
    {
        //DTOJugadoresCustom[] result = await DbContext.Jugadores
        //    .Select((x) => new DTOJugadoresCustom()
        //    {
        //        IdEquipo = x.EquipoId,
        //        Nombre = x.Equipo.Nombre,
        //        Ciudad = x.Equipo.Ciudad,
        //        PromedioEdad = x.Equipo.Jugadores.Average((j) => j.Edad),
        //        ListaJugadores = x.Equipo.Jugadores
        //            .Select((j) => new DTOListaJugadores()
        //            {
        //                IdJugador = j.Id,
        //                Nombre = j.Nombre,
        //                Edad = j.Edad,
        //                Sueldo = j.Sueldo,
        //            }).ToList(),
        //    }).ToArrayAsync();

        DTOJugadoresCustom[] result = await DbContext.Equipos
        .Select((x) => new DTOJugadoresCustom()
        {
            IdEquipo = x.Id,
            Nombre = x.Nombre,
            Ciudad = x.Ciudad,
            PromedioEdad = x.Jugadores.Average((j) => j.Edad),
            ListaJugadores = x.Jugadores
                .Select((j) => new DTOListaJugadores()
                {
                    IdJugador = j.Id,
                    Nombre = j.Nombre,
                    Edad = j.Edad,
                    Sueldo = j.Sueldo,
                }).ToList(),
        }).ToArrayAsync();

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("agrupados")]
    public async Task<IActionResult> GetAgrupados()
    {
        var result = await DbContext.Jugadores
            .GroupBy((x) => x.Lesionado)
            .Select((g) => new
            {
                g.Key,
                TotalLesionados = g.Select((x) => x.Lesionado),
                TotalNoLesionados = g.Select((x) => !x.Lesionado),
                Jugadores = g.Select((x) => new
                {
                    Nombre = g.Select((x) => x.Nombre),
                    Equipo = g.Select((x) => x.Equipo.Nombre),
                }).ToList(),
            }).ToArrayAsync();

        return Ok(result);
    }
    #endregion GET

    #region POST
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DTOJugadorInput input)
    {
        bool existeNombreDB = await DbContext.Jugadores
            .AnyAsync((j) => j.Nombre == input.Nombre);

        if (existeNombreDB)
        {
            return BadRequest($"Ya hay un jugador llamado {input.Nombre}");
        }

        bool existeEquipoDB = await DbContext.Equipos
            .AnyAsync((e) => e.Id == input.EquipoId);

        if (!existeEquipoDB)
        {
            return BadRequest($"No existe ningun equipo con ID {input.EquipoId}");
        }

        Jugadore newItem = new()
        {
            Nombre = input.Nombre,
            Edad = input.Edad,
            Sueldo = input.Sueldo,
            Lesionado = input.Lesionado,
            EquipoId = input.EquipoId
        };

        _ = await DbContext.Jugadores.AddAsync(newItem);
        _ = await DbContext.SaveChangesAsync();

        return NoContent();
    }
    #endregion POST

    #region PUT
    [HttpPut("{pk}")]
    public async Task<IActionResult> Put([FromRoute] int pk, [FromBody] DTOJugadorInput input)
    {
        Jugadore? jugadorDB = await DbContext.Jugadores.AsTracking()
            .FirstOrDefaultAsync((x) => x.Id == pk);

        if (jugadorDB is null)
        {
            return NotFound($"No existe ningun registro con ID: {pk}");
        }

        bool existeEquipoDB = await DbContext.Equipos
            .AnyAsync((e) => e.Id == input.EquipoId);

        if (!existeEquipoDB)
        {
            return BadRequest($"No existe ningun equipo con ID {input.EquipoId}");
        }

        jugadorDB.Nombre = input.Nombre;
        jugadorDB.Edad = input.Edad;
        jugadorDB.Sueldo = input.Sueldo;
        jugadorDB.Lesionado = input.Lesionado;
        jugadorDB.EquipoId = input.EquipoId;

        _ = DbContext.Jugadores.Update(jugadorDB);
        _ = await DbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("aumentar-salario/{pk}")]
    public async Task<IActionResult> Put([FromRoute] int pk, [FromBody] int porcentaje)
    {
        Jugadore? jugadorDB = await DbContext.Jugadores.AsTracking()
            .FirstOrDefaultAsync((x) => x.Id == pk);

        if (jugadorDB is null)
        {
            return NotFound($"No existe ningun registro con ID: {pk}");
        }

        jugadorDB.Sueldo += jugadorDB.Sueldo * porcentaje / 100;

        _ = DbContext.Jugadores.Update(jugadorDB);
        _ = await DbContext.SaveChangesAsync();

        return NoContent();
    }
    #endregion PUT

    #region PATCH
    [HttpPatch("reducir-salario/{porcentaje:int}")]
    public async Task<IActionResult> Patch([FromRoute] int porcentaje)
    //[HttpPatch("reducir-salario")]
    //public async Task<IActionResult> Patch([FromBody] int porcentaje)
    {
        IEnumerable<Jugadore>? updatedItems = await sueldoService.ReducirSueldo(porcentaje);

        if (updatedItems is not null)
        {
            DbContext.Jugadores.UpdateRange(updatedItems);
            _ = await DbContext.SaveChangesAsync();
        }

        return NoContent();
    }
    #endregion PATCH

    #region DELETE

    #endregion DELETE
}
