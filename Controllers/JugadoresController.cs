using EvaluacionDavidLlopis.DTOs;
using EvaluacionDavidLlopis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDavidLlopis.Controllers;
[Route("api/[controller]")]
[ApiController]
public class JugadoresController(LigaContext DbContext) : ControllerBase
{
    #region GET
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Jugadore>>> Get()
    {
        Jugadore[] result = await DbContext.Jugadores.ToArrayAsync();

        return Ok(result);
    }

    [HttpGet("custom")]
    public async Task<ActionResult<IEnumerable<DTOJugadoresCustom>>> GetCustom()
    {
        DTOJugadoresCustom[] result = await DbContext.Jugadores
            .Select((x) => new DTOJugadoresCustom()
            {
                IdEquipo = x.Id,
                Nombre = x.Equipo.Nombre,
                Ciudad = x.Equipo.Ciudad,
                PromedioEdad = x.Equipo.Jugadores.Average((j) => j.Edad),
                ListaJugadores = x.Equipo.Jugadores
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
    #endregion GET

    #region POST

    #endregion POST

    #region PUT

    #endregion PUT

    #region DELETE

    #endregion DELETE
}
