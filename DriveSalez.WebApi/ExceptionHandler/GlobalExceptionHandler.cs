using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace DriveSalez.WebApi.ExceptionHandler;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, message) = GetErrorDetails(exception);

        logger.LogError(exception, "An error occurred: {Message}", exception.Message);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;

        var response = (message, (int)statusCode);
        var jsonResponse = JsonSerializer.Serialize(response);
        
        await httpContext.Response.WriteAsync(jsonResponse, cancellationToken);

        return true;
    }

    private (HttpStatusCode code, string message) GetErrorDetails(Exception exception)
    {
        return exception switch
        {
            ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
            InvalidOperationException => (HttpStatusCode.BadRequest, exception.Message),
            ValidationException => (HttpStatusCode.BadRequest, exception.Message),
            KeyNotFoundException => (HttpStatusCode.BadRequest, exception.Message),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred")
        };
    }
}