using DriveSalez.Shared.Dto.Dto;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IPayPalService
{
    Task<Result<string>> ProcessPayment(PaymentDetails paymentDetails, decimal amount, string currency = "USD");
    Task<Result<bool>> CapturePayment(string orderId);
}