using DriveSalez.Domain.Enums;
using DriveSalez.Shared.Dto.Dto.Services;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface ISubscriptionService
{
    Task<Result<GetSubscriptionResponse>> GetByIdAsync(int id);
    Task<Result<GetSubscriptionResponse>> GetByUserTypeAsync(UserType userType);
    Task AddSubscriptionToUser(int serviceId, Guid baseUserId);
}