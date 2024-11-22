using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface ISubscriptionService
{
    Task<Result<Subscription>> GetByIdAsync(int id);
    Task<Result<Subscription>> GetByUserTypeAsync(UserType userType);
}