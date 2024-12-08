using DriveSalez.Domain.Aggregates;
using DriveSalez.Domain.Aggregates.PaymentAggregate;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IOneTimePurchaseRepository
{
    public Task<OneTimePurchase?> GetByIdAsync(int id);
}