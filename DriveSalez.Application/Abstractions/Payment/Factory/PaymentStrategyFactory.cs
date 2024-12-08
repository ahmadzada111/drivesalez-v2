using DriveSalez.Application.Abstractions.Payment.Strategy;
using DriveSalez.Domain.Common.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace DriveSalez.Application.Abstractions.Payment.Factory;

public class PaymentStrategyFactory(IServiceProvider serviceProvider) : IPaymentStrategyFactory
{
    public IPaymentStrategy GetStrategy(PurchaseType paymentType)
    {
        var strategies = serviceProvider.GetServices<IPaymentStrategy>();
        var strategy = strategies.FirstOrDefault(s => s.PaymentType == paymentType);
        if (strategy == null) throw new NotImplementedException($"No payment strategy found for key {nameof(paymentType)}");
        return strategy;
    }
}