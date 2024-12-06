using DriveSalez.Domain.Enums;
using DriveSalez.Shared.Dto.Dto.Services;

namespace DriveSalez.Application.Abstractions.Payment.Strategy;

public interface IPaymentStrategy
{
    PurchaseType PaymentType { get; }
    Task<GetServiceRequest> GetService(int serviceId);
    Task HandlePostPaymentAsync(int serviceId, Guid baseUserId);
}