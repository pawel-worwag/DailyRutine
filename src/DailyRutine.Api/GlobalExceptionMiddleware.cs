using System;
using System.Net;
using DailyRutine.Shared.Exceptions;

namespace DailyRutine.Api
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Error400Exception ex)
            {
                _logger.LogError($"[{context.TraceIdentifier}] {ex.Message}");
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(ex.Message);
            }
            catch(Error404Exception ex)
            {
                _logger.LogError($"[{context.TraceIdentifier}] {ex.Message}");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsJsonAsync(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{context.TraceIdentifier}] {ex.Message}");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync($"Error message: {ex.Message} \n CorrelationId: {context.TraceIdentifier}");
            }
        }
    }

    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}

