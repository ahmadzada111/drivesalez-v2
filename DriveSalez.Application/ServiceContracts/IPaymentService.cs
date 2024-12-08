using DriveSalez.Application.Dto.Payment;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.ServiceContracts;

public interface IPaymentService
{
    Task<Result<string>> ConfirmPaymentAsync(string orderId);
    Task<Result<PaymentResponse>> InitiatePaymentAsync(PaymentInitiationRequest request, PaymentDetails paymentDetails);
    Task<Result<GetPaymentResponse>> GetPaymentByOrderIdAsync(string orderId);
    Task<Result<string>> CancelPaymentAsync(string orderId);
}