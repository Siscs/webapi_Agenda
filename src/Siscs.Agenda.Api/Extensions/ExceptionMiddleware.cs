 using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Siscs.Agenda.Api.Extensions
{
    public class ExceptionMiddleware
    {
        // para capturar exceptions da aplicacao
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                
                _logger.LogInformation("=================================");
                _logger.LogError(ex.Message.ToString());
                HandleExceptionAsync(httpContext, ex);
            }
        }

        private static void HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // ship to elmah
            //exception.Ship(context); 
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}