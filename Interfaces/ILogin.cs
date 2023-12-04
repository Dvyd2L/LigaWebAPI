using EvaluacionDavidLlopis.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDavidLlopis.Interfaces;

public interface ILogin
{
    public Task<IActionResult> Login([FromBody] DTOUsuario input);
}
