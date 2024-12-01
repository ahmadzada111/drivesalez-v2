using DriveSalez.Domain.Entities;
using DriveSalez.Shared.Dto.Dto.Payment;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IPaymentService
{
    Task<Result<string>> ConfirmPaymentAsync(string orderId);
    Task<Result<PaymentResponse>> InitiatePaymentAsync(PaymentInitiationRequest request, PaymentDetails paymentDetails);
    Task<Result<Payment>> GetPaymentByOrderIdAsync(string orderId);
    Task<Result<string>> CancelPaymentAsync(string orderId);
}