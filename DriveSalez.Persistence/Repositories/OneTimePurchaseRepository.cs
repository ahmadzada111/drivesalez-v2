using DriveSalez.Domain.Aggregates;
using DriveSalez.Domain.Aggregates.PaymentAggregate;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Persistence.Repositories;

internal class OneTimePurchaseRepository(ApplicationDbContext context) : IOneTimePurchaseRepository
{
    public async Task<OneTimePurchase?> GetByIdAsync(int id)
    {
        return await context.OneTimePurchases.FirstOrDefaultAsync(x => x.Id == id);
    }
}