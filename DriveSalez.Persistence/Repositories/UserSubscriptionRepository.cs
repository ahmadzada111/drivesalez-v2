using DriveSalez.Domain.Aggregates.UserAggregate;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

public class UserSubscriptionRepository(ApplicationDbContext context) : IUserSubscriptionRepository
{
    public async Task<UserSubscription> AddAsync(UserSubscription userSubscription)
    {
        var result = await context.UserSubscriptions.AddAsync(userSubscription);
        return result.Entity;
    }
}