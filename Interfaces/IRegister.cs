using EvaluacionDavidLlopis.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDavidLlopis.Interfaces;

public interface IRegister
{
    public Task<IActionResult> Register([FromBody] DTOUsuario input);
}
