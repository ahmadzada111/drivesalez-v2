using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Utilities.Utilities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Services;

public class RoleService(UserManager<ApplicationUser> userManager) : IRoleService
{
    public async Task<Result<IdentityResult>> AddUserToRole(ApplicationUser user, string userRole)
    {
        var result = await userManager.AddToRoleAsync(user, userRole);
        if(result.Succeeded) return Result<IdentityResult>.Success(result);
        var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
        return Result<IdentityResult>.Failure(new Error("User role assignment fail", errors));
    }
}