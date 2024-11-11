using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Shared.Dto.Dto.User;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IUserService
{
    Task<Result<ApplicationUser>> FindByEmail(string email);
    Task<Result<ApplicationUser>> FindByUserName(string userName);
    Task<Result<ApplicationUser>> FindById(Guid userId);
    Task<Result<string>> GenerateEmailConfirmationToken(ApplicationUser identityUser);
    Task<Result<string>> GeneratePasswordResetToken(ApplicationUser identityUser);
    Task<Result<string>> GenerateChangeEmailToken(ApplicationUser identityUser, string newEmail);
    Task<Result<bool>> ConfirmEmail(ApplicationUser identityUser, string token);
    Task<Result<bool>> ResetPassword(ApplicationUser identityUser, string token, string newPassword);

    Task<Result<bool>> CreateUser<TSignUpRequest>(TSignUpRequest request, UserType userType)
        where TSignUpRequest : ISignUpRequest;
    Task<ApplicationUser> ChangeUserRole(ApplicationUser identityUser, UserType userType);
    Task LogOut();
}