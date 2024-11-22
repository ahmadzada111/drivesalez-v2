using DriveSalez.Application.Abstractions.Payment.Strategy;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Application.Abstractions.Payment.Factory;

public interface IPaymentStrategyFactory
{
    IPaymentStrategy GetStrategy(PurchaseType strategy);
}