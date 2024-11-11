using DriveSalez.Domain.IdentityEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DriveSalez.Presentation.Filters;

public class AuthorizedUserFilter(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
    : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = contextAccessor.HttpContext 
                          ?? throw new NullReferenceException("HttpContext is null");

        var user = await userManager.GetUserAsync(httpContext.User);

        if (user == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        httpContext.Items["AuthorizedUser"] = user;

        await next();
    }
}