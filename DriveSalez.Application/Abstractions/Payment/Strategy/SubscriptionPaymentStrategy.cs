using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.Shared.Dto.Dto.Services;

namespace DriveSalez.Application.Abstractions.Payment.Strategy;

public class SubscriptionPaymentStrategy(ISubscriptionService subscriptionService) : IPaymentStrategy
{
    public PurchaseType PaymentType => PurchaseType.Subscription;
    
    public async Task<GetServiceRequest> GetService(int serviceId)
    {
        var result = await subscriptionService.GetByIdAsync(serviceId);
        if (result.IsSuccess) return new GetServiceRequest(result.Value!.Id, result.Value!.Name, result.Value!.Price);
        throw new KeyNotFoundException("Service not found");
    }

    public async Task HandlePostPaymentAsync(int serviceId, Guid userId)
    {
        await subscriptionService.AddSubscriptionToUser(serviceId, userId);
    }
}