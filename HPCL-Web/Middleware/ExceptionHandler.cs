using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace HPCL_Web.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate next;

        public ExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var log = new LoggerConfiguration().WriteTo.File("C:/HPCLLogs/Errorlog.txt").CreateLogger();
            try
            {
                await this.next.Invoke(context);
            }
            catch (Exception ex)
            {
                log.Error(ex,
                      $"Request {context.Request?.Method}: {context.Request?.Path.Value} failed");

                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
                throw;
            }
        }
        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
