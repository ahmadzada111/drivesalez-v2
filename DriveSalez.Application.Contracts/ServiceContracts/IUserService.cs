using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Shared.Dto.Dto.User;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IUserService
{
    Task<Result<TUser>> FindBaseUserByIdAsync<TUser>(Guid baseUserId) where TUser : BaseUser;
    Task<Result<ApplicationUser>> FindIdentityUserByEmailAsync(string email);
    Task<Result<ApplicationUser>> FindIdentityUserByUserNameAsync(string userName);
    Task<Result<ApplicationUser>> FindIdentityUserByIdAsync(Guid userId);
    Task<Result<string>> GenerateEmailConfirmationTokenAsync(ApplicationUser identityUser);
    Task<Result<string>> GeneratePasswordResetTokenAsync(ApplicationUser identityUser);
    Task<Result<string>> GenerateChangeEmailTokenAsync(ApplicationUser identityUser, string newEmail);
    Task<Result<bool>> ConfirmEmailAsync(ApplicationUser identityUser, string token);
    Task<Result<bool>> ResetPasswordAsync(ApplicationUser identityUser, string token, string newPassword);
    Task<Result<bool>> CreateUserAsync<TSignUpRequest>(TSignUpRequest request, UserType userType)
        where TSignUpRequest : ISignUpRequest;
    Task<ApplicationUser> ChangeUserRoleAsync(ApplicationUser identityUser, UserType userType);
    Task LogOutAsync();
}