using EvaluacionDavidLlopis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDavidLlopis.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EquiposController(LigaContext DbContext) : ControllerBase
{
    #region GET
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipo>>> Get()
    {
        Equipo[] result = await DbContext.Equipos.ToArrayAsync();

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
