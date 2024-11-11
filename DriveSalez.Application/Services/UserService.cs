using DriveSalez.Application.Abstractions;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Shared.Dto.Dto.User;
using DriveSalez.Utilities.Utilities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Services;

internal class UserService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    UserFactorySelector userFactorySelector) : IUserService
{
    public async Task<Result<ApplicationUser>> FindByEmail(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null) return Result<ApplicationUser>.Failure(UserErrors.NotFound);
        return Result<ApplicationUser>.Success(user);
    }

    public async Task<Result<ApplicationUser>> FindByUserName(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        if(user is null) return Result<ApplicationUser>.Failure(UserErrors.NotFound);
        return Result<ApplicationUser>.Success(user);
    }
    
    public async Task<Result<ApplicationUser>> FindById(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if(user is null) return Result<ApplicationUser>.Failure(UserErrors.NotFound);
        return Result<ApplicationUser>.Success(user);

    }
    
    public async Task<Result<string>> GenerateEmailConfirmationToken(ApplicationUser identityUser)
    {
        return Result<string>.Success(await userManager.GenerateEmailConfirmationTokenAsync(identityUser));
    }

    public async Task<Result<string>> GeneratePasswordResetToken(ApplicationUser identityUser)
    {
        return Result<string>.Success(await userManager.GeneratePasswordResetTokenAsync(identityUser));
    }

    public async Task<Result<string>> GenerateChangeEmailToken(ApplicationUser identityUser, string newEmail)
    {
        return Result<string>.Success(await userManager.GenerateChangeEmailTokenAsync(identityUser, newEmail));
    }
    
    public async Task<Result<bool>> ConfirmEmail(ApplicationUser identityUser, string token)
    {
        var result = await userManager.ConfirmEmailAsync(identityUser, token);
        if (!result.Succeeded)
        {
            var message = string.Join(" | ", result.Errors);
            return Result<bool>.Failure(new Error("Failed to confirm email.", message));
        }
        return Result<bool>.Success(true);
    }
    
    public async Task<Result<bool>> CreateUser<TSignUpRequest>(TSignUpRequest request, UserType userType) where TSignUpRequest : ISignUpRequest
    {
        var userFactory = userFactorySelector.GetFactory<TSignUpRequest>();
        var user = userFactory.CreateUser(request);
        
        var identityUser = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email,
            PhoneNumber = request.PhoneNumber,
            BaseUser = user
        };

        IdentityResult result = await userManager.CreateAsync(identityUser, request.Password);

        if (!result.Succeeded)
        {
            var message = string.Join(" | ", result.Errors.Select(e => e.Description));
            return Result<bool>.Failure(new Error("User Creation Failed", message));
        }

        await userManager.AddToRoleAsync(identityUser, userType.ToString());
        await userManager.UpdateAsync(identityUser);
        // await _limitService.AddLimitToUser(user, userType);
        return Result<bool>.Success(true);
    }
    
    public async Task<Result<bool>> ResetPassword(ApplicationUser identityUser, string token, string newPassword)
    {
        var result =  await userManager.ResetPasswordAsync(identityUser, token, newPassword);
        if (!result.Succeeded)
        {
            var message = string.Join(" | ", result.Errors);
            return Result<bool>.Failure(new Error("Failed to reset password.", message));
        }
        return Result<bool>.Success(true);
    }
    
    public async Task<ApplicationUser> ChangeUserRole(ApplicationUser identityUser, UserType userType)
    {
        var roles = await userManager.GetRolesAsync(identityUser);
        await userManager.RemoveFromRolesAsync(identityUser, roles);
        
        await userManager.AddToRoleAsync(identityUser, userType.ToString());
        await userManager.UpdateAsync(identityUser);

        return identityUser;
    }
    
    public async Task LogOut()
    {
        await signInManager.SignOutAsync();
    }
}