using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.DbContext;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Persistence.Repositories;

internal class OneTimePurchaseRepository(ApplicationDbContext context) : IOneTimePurchaseRepository
{
    public async Task<OneTimePurchase?> GetByIdAsync(int id)
    {
        return await context.OneTimePurchases.FirstOrDefaultAsync(x => x.Id == id);
    }
}