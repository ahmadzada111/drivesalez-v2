using DriveSalez.Domain.Entities;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IOneTimePurchaseRepository
{
    public Task<OneTimePurchase?> GetByIdAsync(int id);
}