using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Common.Enums;

namespace DriveSalez.WebApi.Middleware;

public class CheckUserStatusMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
{
    public async Task InvokeAsync(HttpContext context)
    {
        using var scope = serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var jwtService = scope.ServiceProvider.GetRequiredService<IJwtService>();

        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var credentials = jwtService.GetPrincipalFromJwtToken(token);
            var customUserId = credentials.Claims.FirstOrDefault(x => x.Type == "CustomUserId");

            if (customUserId == null)
            {
                context.Response.StatusCode = 401;
                return;
            }

            var user = await userService.FindCustomUserByIdAsync(Guid.Parse(customUserId.Value));
            if (!user.IsSuccess)
            {
                context.Response.StatusCode = 401;
                return;
            }

            if (user.Value!.UserStatus != UserStatus.Active)
            {
                context.Response.StatusCode = 403;
                return;
            }
        }

        await next(context);
    }
}