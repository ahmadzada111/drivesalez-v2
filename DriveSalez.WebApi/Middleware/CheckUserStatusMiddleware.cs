using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.WebApi.Middleware;

public class CheckUserStatusMiddleware(
    RequestDelegate next, 
    IUserService userService)
{

    public async Task InvokeAsync(HttpContext context)
    {
        
        
        await next(context);
    }
}