using DriveSalez.Domain.Entities;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IOneTimePurchaseService
{
    public Task<Result<OneTimePurchase>> GetByIdAsync(int id);
}