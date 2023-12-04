namespace EvaluacionDavidLlopis.Middlewares;

public class LogRequestMiddleware(RequestDelegate next, IWebHostEnvironment env)
{
    #region PROPs
    private readonly string logFile = $@"{env.ContentRootPath}\wwwroot\log.txt";
    private readonly string IPv6Baneada = "::1";
    #endregion

    #region METHODs
    /// <summary>
    /// Invoke o InvokeAsync
    /// Este método se va a ejecutar automáticamente en cada petición porque en el program hemos registrado el middleware así:
    /// <![CDATA[app.UseMiddleware<IpBlockerMiddleware>();]]>
    /// </summary>
    /// <param name="httpContext">tiene información de la petición que viene</param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        string IP = httpContext.Connection.RemoteIpAddress?.ToString() ?? "NotAvailable";
        string metodo = httpContext.Request.Method;
        string ruta = httpContext.Request.Path.ToString();

        LogWriter(IP, metodo, ruta);
        await PostIPBlocker(httpContext, IP, metodo);

        await next(httpContext);
    }

    private void LogWriter(string IP, string metodo, string ruta)
    {
        using (StreamWriter writer = new(logFile))
        {
            writer.WriteLine($"{DateTime.Now} - {IP} - {metodo} - {ruta}");
        }
    }

    private async Task PostIPBlocker(HttpContext httpContext, string IP, string metodo)
    {
        bool isInvalidMethod = HttpMethods.IsPost(metodo);

        if (IP == IPv6Baneada && isInvalidMethod) // Bloquearía las peticiones POST de una IP concreta
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.ContentType = "plain/text";
            await httpContext.Response.WriteAsync("No tienes derecho");
        }
    }
    #endregion
}
