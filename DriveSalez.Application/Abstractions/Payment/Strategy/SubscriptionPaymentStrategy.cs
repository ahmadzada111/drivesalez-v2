using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Application.Abstractions.Payment.Strategy;

public class SubscriptionPaymentStrategy(ISubscriptionService subscriptionService) : IPaymentStrategy
{
    public PurchaseType PaymentType => PurchaseType.Subscription;
    
    public Task<decimal> GetAmountAsync(int serviceId)
    {
        throw new NotImplementedException();
    }

    public Task HandlePostPaymentAsync(int serviceId, Guid userId)
    {
        throw new NotImplementedException();
    }
}