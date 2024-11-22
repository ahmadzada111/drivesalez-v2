using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Persistence.DbContext;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Persistence.Repositories;

internal class SubscriptionRepository(ApplicationDbContext context) : ISubscriptionRepository
{
    public async Task<Subscription?> GetByIdAsync(int id)
    {
        return await context.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Subscription?> GetByUserTypeAsync(UserType userType)
    {
        return await context.Subscriptions.FirstOrDefaultAsync(x => x.UserType == userType);
    }
}