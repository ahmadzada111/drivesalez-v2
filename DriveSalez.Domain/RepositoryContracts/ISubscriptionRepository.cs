using DriveSalez.Domain.Aggregates;
using DriveSalez.Domain.Aggregates.PaymentAggregate;
using DriveSalez.Domain.Common.Enums;

namespace DriveSalez.Domain.RepositoryContracts;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(int id);
    Task<Subscription?> GetByUserTypeAsync(UserType userType);
}