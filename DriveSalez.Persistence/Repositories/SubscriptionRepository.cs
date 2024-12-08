using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Persistence.Repositories;

internal class SubscriptionRepository(ApplicationDbContext context) : ISubscriptionRepository
{
    public async Task<Subscription?> GetByIdAsync(int id)
    {
        return await context.Subscriptions
            .Include(x => x.SubscriptionLimits)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Subscription?> GetByUserTypeAsync(UserType userType)
    {
        return await context.Subscriptions
            .Include(x => x.SubscriptionLimits)
            .FirstOrDefaultAsync(x => x.UserType == userType);
    }
}