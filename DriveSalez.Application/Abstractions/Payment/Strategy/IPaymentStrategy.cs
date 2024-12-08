using DriveSalez.Application.Dto.Services;
using DriveSalez.Domain.Common.Enums;

namespace DriveSalez.Application.Abstractions.Payment.Strategy;

public interface IPaymentStrategy
{
    PurchaseType PaymentType { get; }
    Task<GetServiceRequest> GetService(int serviceId);
    Task HandlePostPaymentAsync(int serviceId, Guid baseUserId);
}