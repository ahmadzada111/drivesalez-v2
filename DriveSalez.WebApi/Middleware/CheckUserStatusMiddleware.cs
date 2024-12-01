using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;

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
            var baseUserId = credentials.Claims.FirstOrDefault(x => x.Type == "BaseUserId");

            if (baseUserId == null)
            {
                context.Response.StatusCode = 401;
                return;
            }

            var user = await userService.FindBaseUserByIdAsync<User>(Guid.Parse(baseUserId.Value));
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