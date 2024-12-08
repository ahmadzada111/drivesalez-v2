using DriveSalez.Application.Dto.Services;
using DriveSalez.Domain.Enums;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.ServiceContracts;

public interface ISubscriptionService
{
    Task<Result<GetSubscriptionResponse>> GetByIdAsync(int id);
    Task<Result<GetSubscriptionResponse>> GetByUserTypeAsync(UserType userType);
    Task AddSubscriptionToUser(int serviceId, Guid baseUserId);
}