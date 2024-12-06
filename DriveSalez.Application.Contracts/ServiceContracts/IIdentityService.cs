using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Utilities.Utilities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IIdentityService
{
    Task<Result<IdentityResult>> CreateUserAsync(ApplicationUser user, string password);
    Task<Result<IdentityResult>> UpdateUserAsync(ApplicationUser user);
    Task<Result<ApplicationUser>> FindIdentityUserByEmailAsync(string email);
    Task<Result<ApplicationUser>> FindIdentityUserByUserNameAsync(string userName);
    Task<Result<ApplicationUser>> FindIdentityUserByIdAsync(Guid userId);
    Task<Result<string>> GenerateEmailConfirmationTokenAsync(ApplicationUser identityUser);
    Task<Result<string>> GeneratePasswordResetTokenAsync(ApplicationUser identityUser);
    Task<Result<string>> GenerateChangeEmailTokenAsync(ApplicationUser identityUser, string newEmail);
    Task<Result<bool>> ConfirmEmailAsync(ApplicationUser identityUser, string token);
    Task<Result<bool>> ResetPasswordAsync(ApplicationUser identityUser, string token, string newPassword);
    Task<ApplicationUser> ChangeUserRoleAsync(ApplicationUser identityUser, string userRole);
    Task SignOutAsync();
}