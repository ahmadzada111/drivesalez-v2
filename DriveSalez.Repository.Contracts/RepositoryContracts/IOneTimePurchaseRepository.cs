using DriveSalez.Domain.Entities;

namespace DriveSalez.Repository.Contracts.RepositoryContracts;

public interface IOneTimePurchaseRepository
{
    public Task<OneTimePurchase?> GetByIdAsync(int id);
}