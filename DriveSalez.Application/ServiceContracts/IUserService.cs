using DriveSalez.Application.Dto.User;
using DriveSalez.Domain.Aggregates.UserAggregate;
using DriveSalez.Domain.Common.Enums;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.ServiceContracts;

public interface IUserService
{
    Task<Result<CustomUser>> AddCustomUserAsync(CustomUser customUser);
    Task<Result<CustomUser>> FindCustomUserByIdAsync(Guid baseUserId);
    Task<Result<CustomUser>> UpdateCustomUserAsync(CustomUser baseCustomUser);
    Task<Result<Guid>> CreateUserAsync<TSignUpRequest>(TSignUpRequest request, UserType userType) where TSignUpRequest : ISignUpRequest;
    Task<Result<Guid>> CompleteBusinessSignUpAsync(Guid pendingUserId, string orderId);
}