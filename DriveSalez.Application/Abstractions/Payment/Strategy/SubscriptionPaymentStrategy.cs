using DriveSalez.Application.Dto.Services;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Common.Enums;

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

    public async Task HandlePostPaymentAsync(int serviceId, Guid baseUserId)
    {
        await subscriptionService.AddSubscriptionToUser(serviceId, baseUserId);
    }
}