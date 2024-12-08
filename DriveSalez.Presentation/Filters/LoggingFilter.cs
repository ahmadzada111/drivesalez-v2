using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace DriveSalez.Presentation.Filters;

public class LoggingFilter : IActionFilter
{
    private readonly ILogger<LoggingFilter> _logger;

    public LoggingFilter(ILogger<LoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;
        var routeData = context.RouteData;
        var actionArguments = context.ActionArguments;

        _logger.LogInformation("----- [Action Started] -----");
        _logger.LogInformation($"[{DateTime.Now}] HTTP {request.Method} {request.Path}");
        _logger.LogInformation($"Route Data: {string.Join(", ", routeData.Values.Select(k => $"{k.Key}={k.Value}"))}");
        _logger.LogInformation($"Action Arguments: {string.Join(", ", actionArguments.Select(k => $"{k.Key}={k.Value}"))}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            _logger.LogError(context.Exception, "----- [An Exception Occurred] -----");
        }
        else
        {
            var response = context.HttpContext.Response;
            _logger.LogInformation("----- [Action Ended] -----");
            _logger.LogInformation($"[{DateTime.Now}] Response Status Code: {response.StatusCode}");
        }

        _logger.LogInformation("----- [Request Completed] -----");
    }
}