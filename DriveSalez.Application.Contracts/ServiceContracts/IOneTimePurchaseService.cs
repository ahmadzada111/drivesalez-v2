using DriveSalez.Shared.Dto.Dto.Services;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IOneTimePurchaseService
{
    Task<Result<GetOneTimePurchaseResponse>> GetByIdAsync(int id);
    Task AddOneTimePurchaseToUser(int serviceId, Guid userId);
}