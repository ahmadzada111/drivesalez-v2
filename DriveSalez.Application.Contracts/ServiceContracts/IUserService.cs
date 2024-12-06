using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Shared.Dto.Dto.User;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IUserService
{
    Task<Result<TUser>> AddBaseUserAsync<TUser>(TUser user) where TUser : BaseUser;
    Task<Result<TUser>> FindBaseUserByIdAsync<TUser>(Guid baseUserId) where TUser : BaseUser;
    Task<Result<TUser>> UpdateBaseUserAsync<TUser>(TUser baseUser) where TUser : BaseUser;
    Task<Result<Guid>> CreateUserAsync<TSignUpRequest>(TSignUpRequest request, UserType userType) where TSignUpRequest : ISignUpRequest;
    Task<Result<Guid>> CompleteBusinessSignInAsync(Guid pendingUserId, string orderId);
}