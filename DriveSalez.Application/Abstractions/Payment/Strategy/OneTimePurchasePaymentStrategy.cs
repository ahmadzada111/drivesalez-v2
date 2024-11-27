using DriveSalez.Domain.Enums;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using DriveSalez.Shared.Dto.Dto.Services;

namespace DriveSalez.Application.Abstractions.Payment.Strategy;

public class OneTimePurchasePaymentStrategy(IUnitOfWork unitOfWork) : IPaymentStrategy
{
    public PurchaseType PaymentType => PurchaseType.OneTimePurchase;
    
    public async Task<GetServiceRequest> GetService(int serviceId)
    {
        var result = await unitOfWork.OneTimePurchaseRepository.GetByIdAsync(serviceId);
        if (result is not null) return new GetServiceRequest(result.Id, result.Name, result.Price);
        throw new KeyNotFoundException("Service not found");
    }

    public async Task HandlePostPaymentAsync(int serviceId, Guid userId)
    {
        var service = await unitOfWork.OneTimePurchaseRepository.GetByIdAsync(serviceId);
        if(service is null) throw new KeyNotFoundException("Service not found");

        var user = await unitOfWork.UserRepository.GetByIdAsync<Domain.IdentityEntities.User>(userId);
        if(user is null) throw new KeyNotFoundException("User not found");
        
        user.OneTimePurchases.Add(service);
        unitOfWork.UserRepository.Update(user);
    }
}