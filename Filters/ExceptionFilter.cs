using Microsoft.AspNetCore.Mvc.Filters;

namespace EvaluacionDavidLlopis.Filters;

public class ExceptionFilter(IWebHostEnvironment env) : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        string path = $@"{env.ContentRootPath}\wwwroot\log-errores.txt";

        string IP = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
        string metodo = context.HttpContext.Request.Method;
        string ruta = context.HttpContext.Request.Path.ToString();
        string error = context.Exception.Message;

        using (StreamWriter writer = new(path, append: true))
        {
            //writer.WriteLine(context.Exception.Source);
            //writer.WriteLine(context.Exception.Message);
            writer.WriteLine($@"{IP} - {metodo} - {ruta} - {error} - {DateTime.Now}");
        }

        base.OnException(context);
    }
}
