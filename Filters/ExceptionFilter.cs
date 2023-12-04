using Microsoft.AspNetCore.Mvc.Filters;

namespace EvaluacionDavidLlopis.Filters;

public class ExceptionFilter(IWebHostEnvironment env)
    : ExceptionFilterAttribute
{
    private readonly string _logFilePath = $@"{env.ContentRootPath}\wwwroot\logErrores.txt";

    public override void OnException(ExceptionContext context)
    {
        string IP = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
        string metodo = context.HttpContext.Request.Method;
        string ruta = context.HttpContext.Request.Path.ToString();
        string error = context.Exception.Message;

        using (StreamWriter writer = new(_logFilePath, append: true))
        {
            writer.WriteLine($@"{IP} - {metodo} - {ruta} - {error} - {DateTime.Now}");
        }

        base.OnException(context);
    }
}
