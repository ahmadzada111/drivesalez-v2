using DriveSalez.Application.Dto.Payment;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.ServiceContracts;

public interface IPayPalService
{
    Task<Result<PaymentResponse>> ProcessPaymentAsync(PaymentDetails paymentDetails, decimal amount, string currency = "USD");
    Task<Result<bool>> CapturePaymentAsync(string orderId);
}