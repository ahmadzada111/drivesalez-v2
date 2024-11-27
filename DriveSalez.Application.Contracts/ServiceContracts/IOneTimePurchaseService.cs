using DriveSalez.Domain.Entities;
using DriveSalez.Shared.Dto.Dto.Services;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IOneTimePurchaseService
{
    public Task<Result<GetOneTimePurchaseRequest>> GetByIdAsync(int id);
}