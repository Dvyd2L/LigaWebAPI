using EvaluacionDavidLlopis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDavidLlopis.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EquiposController(LigaContext DbContext) : ControllerBase
{
    #region GET ✅
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipo>>> Get()
    {
        Equipo[] result = await DbContext.Equipos.ToArrayAsync();

        return Ok(result);
    }
    #endregion GET

    #region DELETE ✅
    [Authorize]
    [HttpDelete("{pk}")]
    public async Task<IActionResult> Delete([FromRoute] int pk)
    {
        Equipo? equipoDB = await DbContext.Equipos
            .Include((x) => x.Jugadores)
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == pk);

        if (equipoDB is null)
        {
            return NotFound($"No se encontro ningun equipo con ID: {pk}");
        }

        if (equipoDB.Jugadores.Count > 0)
        {
            return BadRequest("No se puede eliminar un equipo con jugadores");
        }

        _ = DbContext.Equipos.Remove(equipoDB);
        _ = await DbContext.SaveChangesAsync();

        return NoContent();
    }

    [Authorize]
    [HttpDelete("full/{pk}")]
    public async Task<IActionResult> DeleteFull([FromRoute] int pk)
    {
        Equipo? equipoDB = await DbContext.Equipos
            .Include((x) => x.Jugadores)
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == pk);

        if (equipoDB is null)
        {
            return NotFound($"No se encontro ningun equipo con ID: {pk}");
        }

        if (equipoDB.Jugadores.Count > 0)
        {
            DbContext.Jugadores.RemoveRange(equipoDB.Jugadores);
        }

        _ = DbContext.Equipos.Remove(equipoDB);
        _ = await DbContext.SaveChangesAsync();

        return NoContent();
    }
    #endregion DELETE
}
