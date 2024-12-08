using DriveSalez.Domain.Aggregates.UserAggregate;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IUserSubscriptionRepository
{
    Task<UserSubscription> AddAsync(UserSubscription userSubscription);
}