using DriveSalez.Domain.Entities;

namespace DriveSalez.Repository.Contracts.RepositoryContracts;

public interface IUserSubscriptionRepository
{
    Task<UserSubscription> AddAsync(UserSubscription userSubscription);
}