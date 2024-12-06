using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.DbContext;
using DriveSalez.Repository.Contracts.RepositoryContracts;

namespace DriveSalez.Persistence.Repositories;

public class UserSubscriptionRepository(ApplicationDbContext context) : IUserSubscriptionRepository
{
    public async Task<UserSubscription> AddAsync(UserSubscription userSubscription)
    {
        var result = await context.UserSubscriptions.AddAsync(userSubscription);
        return result.Entity;
    }
}