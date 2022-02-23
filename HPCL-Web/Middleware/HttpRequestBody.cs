using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Middleware
{
    public class HttpRequestBody
    { 
    private readonly RequestDelegate next;

    public HttpRequestBody(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
            var log = new LoggerConfiguration().WriteTo.File("C:/HPCLLogs/Infolog.txt").CreateLogger();
            context.Request.EnableBuffering();

        var reader = new StreamReader(context.Request.Body);

        string body = await reader.ReadToEndAsync();
        log.Information(
            $"Request {context.Request?.Method}: {context.Request?.Path.Value}\n{body}");

        context.Request.Body.Position = 0L;

        await next(context);
    }
}
}