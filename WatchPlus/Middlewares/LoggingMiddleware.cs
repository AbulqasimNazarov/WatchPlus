using Microsoft.AspNetCore.Mvc;
using WatchPlus.Models;
using WatchPlus.Services.Base;

namespace WatchPlus.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly IServiceProvider serviceProvider;

    public LoggingMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        this.next = next;
        this.serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext httpContext, IConfiguration configuration)
    {
        var logFlag = configuration["LogSettings:LogFlag"];
        if (logFlag == "false")
        {
            await this.next(httpContext);
            return;
        }
        else
        {
            await this.next(httpContext);

            using (var scope = serviceProvider.CreateScope())
            {
                var logService = scope.ServiceProvider.GetRequiredService<ILogService>();
                var logObj = new Log
                {
                    Url = httpContext.Request.Path,
                    ResponseBody = httpContext.Response.Body.ToString(),
                    RequestBody = httpContext.Request.Body.ToString(),
                    StatusCode = httpContext.Response.StatusCode.ToString(),
                    HttpMethod = httpContext.Request.Method,
                };
                await logService.CreateNewLogAsync(logObj);
                System.Console.WriteLine(httpContext.Request.Path);
            }
        }
    }
}
