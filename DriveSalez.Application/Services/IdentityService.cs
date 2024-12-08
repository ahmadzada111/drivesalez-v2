using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Utilities.Utilities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Services;

internal class IdentityService(
    UserManager<ApplicationUser> userManager, 
    SignInManager<ApplicationUser> signInManager) : IIdentityService
{
    public async Task<Result<IdentityResult>> CreateUserAsync(ApplicationUser user, string password)
    {
        var result = await userManager.CreateAsync(user, password);
        if(result.Succeeded) return Result<IdentityResult>.Success(result);
        var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
        return Result<IdentityResult>.Failure(UserErrors.UserCreationFailed(errors));
    }

    public async Task<Result<IdentityResult>> UpdateUserAsync(ApplicationUser user)
    {
        var result = await userManager.UpdateAsync(user);
        if(result.Succeeded) return Result<IdentityResult>.Success(result);
        var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
        return Result<IdentityResult>.Failure(UserErrors.UserUpdateFailed(errors));
    }

    public async Task<Result<ApplicationUser>> FindIdentityUserByEmailAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null) return Result<ApplicationUser>.Failure(UserErrors.NotFound);
        return Result<ApplicationUser>.Success(user);
    }

    public async Task<Result<ApplicationUser>> FindIdentityUserByUserNameAsync(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        if(user is null) return Result<ApplicationUser>.Failure(UserErrors.NotFound);
        return Result<ApplicationUser>.Success(user);
    }
    
    public async Task<Result<ApplicationUser>> FindIdentityUserByIdAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if(user is null) return Result<ApplicationUser>.Failure(UserErrors.NotFound);
        return Result<ApplicationUser>.Success(user);
    }
    
    public async Task<Result<string>> GenerateEmailConfirmationTokenAsync(ApplicationUser identityUser)
    {
        return Result<string>.Success(await userManager.GenerateEmailConfirmationTokenAsync(identityUser));
    }

    public async Task<Result<string>> GeneratePasswordResetTokenAsync(ApplicationUser identityUser)
    {
        return Result<string>.Success(await userManager.GeneratePasswordResetTokenAsync(identityUser));
    }

    public async Task<Result<string>> GenerateChangeEmailTokenAsync(ApplicationUser identityUser, string newEmail)
    {
        return Result<string>.Success(await userManager.GenerateChangeEmailTokenAsync(identityUser, newEmail));
    }
    
    public async Task<Result<bool>> ConfirmEmailAsync(ApplicationUser identityUser, string token)
    {
        var result = await userManager.ConfirmEmailAsync(identityUser, token);
        if (!result.Succeeded)
        {
            var errors = string.Join(" | ", result.Errors);
            return Result<bool>.Failure(UserErrors.UserCreationFailed(errors));
        }
        return Result<bool>.Success(true);
    }
    
    public async Task<Result<bool>> ResetPasswordAsync(ApplicationUser identityUser, string token, string newPassword)
    {
        var result =  await userManager.ResetPasswordAsync(identityUser, token, newPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(" | ", result.Errors);
            return Result<bool>.Failure(UserErrors.PasswordResetFailed(errors));
        }
        return Result<bool>.Success(true);
    }
    
    public async Task<ApplicationUser> ChangeUserRoleAsync(ApplicationUser identityUser, string userRole)
    {
        var roles = await userManager.GetRolesAsync(identityUser);
        await userManager.RemoveFromRolesAsync(identityUser, roles);
        
        await userManager.AddToRoleAsync(identityUser, userRole);
        await userManager.UpdateAsync(identityUser);

        return identityUser;
    }
    
    public async Task SignOutAsync()
    {
        await signInManager.SignOutAsync();
    }
}