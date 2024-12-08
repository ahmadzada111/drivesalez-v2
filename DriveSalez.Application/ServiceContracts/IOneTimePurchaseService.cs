using DriveSalez.Application.Dto.Services;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.ServiceContracts;

public interface IOneTimePurchaseService
{
    Task<Result<GetOneTimePurchaseResponse>> GetByIdAsync(int id);
    Task AddOneTimePurchaseToUser(int serviceId, Guid userId);
}