using DriveSalez.Application.Abstractions.User;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using DriveSalez.Shared.Dto.Dto.User;
using DriveSalez.Utilities.Utilities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Services;

internal class UserService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    UserFactorySelector userFactorySelector,
    IUnitOfWork unitOfWork,
    ISubscriptionService subscriptionService,
    IUserLimitService userLimitService) : IUserService
{
    public async Task<Result<TUser>> FindBaseUserByIdAsync<TUser>(Guid baseUserId) where TUser : BaseUser
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync<TUser>(baseUserId);
        if (user is null) return Result<TUser>.Failure(UserErrors.NotFound);
        return Result<TUser>.Success(user);
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
            var message = string.Join(" | ", result.Errors);
            return Result<bool>.Failure(new Error("Failed to confirm email.", message));
        }
        return Result<bool>.Success(true);
    }
    
    public async Task<Result<bool>> CreateUserAsync<TSignUpRequest>(TSignUpRequest request, UserType userType) where TSignUpRequest : ISignUpRequest
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

        var service = await subscriptionService.GetByUserTypeAsync(userType);
        if(!service.IsSuccess) return Result<bool>.Failure(UserErrors.NotFound);
        await userManager.AddToRoleAsync(identityUser, userType.ToString());
        await userManager.UpdateAsync(identityUser);

        await unitOfWork.BeginTransactionAsync();
        try
        {
            user.Subscription = service.Value!;
            unitOfWork.UserRepository.AddAsync(user);
            await userLimitService.AddUserLimitToUser(user.Id, service.Value!.RegularUploadLimit, LimitType.Regular);
            await userLimitService.AddUserLimitToUser(user.Id, service.Value!.PremiumUploadLimit, LimitType.Premium);
            await unitOfWork.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
        
        return Result<bool>.Success(true);
    }
    
    public async Task<Result<bool>> ResetPasswordAsync(ApplicationUser identityUser, string token, string newPassword)
    {
        var result =  await userManager.ResetPasswordAsync(identityUser, token, newPassword);
        if (!result.Succeeded)
        {
            var message = string.Join(" | ", result.Errors);
            return Result<bool>.Failure(new Error("Failed to reset password.", message));
        }
        return Result<bool>.Success(true);
    }
    
    public async Task<ApplicationUser> ChangeUserRoleAsync(ApplicationUser identityUser, UserType userType)
    {
        var roles = await userManager.GetRolesAsync(identityUser);
        await userManager.RemoveFromRolesAsync(identityUser, roles);
        
        await userManager.AddToRoleAsync(identityUser, userType.ToString());
        await userManager.UpdateAsync(identityUser);

        return identityUser;
    }
    
    public async Task LogOutAsync()
    {
        await signInManager.SignOutAsync();
    }
}