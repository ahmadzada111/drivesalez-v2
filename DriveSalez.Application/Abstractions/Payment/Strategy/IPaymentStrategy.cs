using DriveSalez.Domain.Enums;

namespace DriveSalez.Application.Abstractions.Payment.Strategy;

public interface IPaymentStrategy
{
    PurchaseType PaymentType { get; }
    Task<decimal> GetAmountAsync(int serviceId);
    Task HandlePostPaymentAsync(int serviceId, Guid userId);
}