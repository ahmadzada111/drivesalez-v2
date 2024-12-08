using DriveSalez.Application.Dto.Services;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Aggregates;
using DriveSalez.Domain.Aggregates.PaymentAggregate;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Services;

internal class OneTimePurchaseService(IUnitOfWork unitOfWork) : IOneTimePurchaseService
{
    public async Task<Result<GetOneTimePurchaseResponse>> GetByIdAsync(int id)
    {
        var result = await unitOfWork.OneTimePurchaseRepository.GetByIdAsync(id);
        if (result is null) return Result<GetOneTimePurchaseResponse>.Failure(new Error(nameof(OneTimePurchase), $"Id {id} is invalid"));
        return Result<GetOneTimePurchaseResponse>.Success((GetOneTimePurchaseResponse)result);
    }

    public async Task AddOneTimePurchaseToUser(int serviceId, Guid userId)
    {
        var service = await unitOfWork.OneTimePurchaseRepository.GetByIdAsync(serviceId);
        if(service is null) throw new KeyNotFoundException("Service not found");
        
        var user = await unitOfWork.UserRepository.GetByIdAsync(userId);
        if(user is null) throw new KeyNotFoundException("User not found");
        
        user.AddOneTimePurchase(service.LimitType, service.LimitValue, service.Name, service.Price);
        unitOfWork.UserRepository.Update(user);
        await unitOfWork.SaveChangesAsync();
    }
}