using DriveSalez.Domain.Entities;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IUserSubscriptionRepository
{
    Task<UserSubscription> AddAsync(UserSubscription userSubscription);
}