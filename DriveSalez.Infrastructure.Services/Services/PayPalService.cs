using System.Globalization;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Shared.Dto.Dto.Payment;
using DriveSalez.Utilities.Settings;
using DriveSalez.Utilities.Utilities;
using Microsoft.Extensions.Options;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

namespace DriveSalez.Infrastructure.Services.Services;

public class PayPalService : IPayPalService
{
    private readonly PayPalHttpClient _client;

    public PayPalService(IOptions<PayPalSettings> settings)
    {
        var payPalSettings = settings.Value;
        var environment = new SandboxEnvironment(
            payPalSettings.ClientId,
            payPalSettings.Secret
        );
        _client = new PayPalHttpClient(environment);
    }

    public async Task<Result<PaymentResponse>> ProcessPaymentAsync(PaymentDetails paymentDetails, decimal amount, string currency = "USD")
    {
        var orderRequest = new OrderRequest
        {
            CheckoutPaymentIntent = "CAPTURE",
            ApplicationContext = new ApplicationContext
            {
                ReturnUrl = paymentDetails.ReturnUrl,
                CancelUrl = paymentDetails.CancelUrl,
                BrandName = "DriveSalez",
                LandingPage = "BILLING",
                UserAction = "PAY_NOW",
                ShippingPreference = "NO_SHIPPING"
            },
            PurchaseUnits =
            [
                new PurchaseUnitRequest
                {
                    AmountWithBreakdown = new AmountWithBreakdown
                    {
                        CurrencyCode = currency,
                        Value = amount.ToString("F2", CultureInfo.InvariantCulture)
                    }
                }
            ]
        };

        var request = new OrdersCreateRequest();
        request.Prefer("return=representation");
        request.RequestBody(orderRequest);

        var response = await _client.Execute(request);
        var result = response.Result<Order>();
        var approvalLink = result.Links.FirstOrDefault(link => link.Rel.Equals("approve", StringComparison.OrdinalIgnoreCase))?.Href;

        if (!string.IsNullOrEmpty(approvalLink)) return Result<PaymentResponse>.Success(new PaymentResponse(result.Id, approvalLink));
        return Result<PaymentResponse>.Failure(new Error("Payment Error", "Approval URL not found."));
    }

    public async Task<Result<bool>> CapturePaymentAsync(string orderId)
    {
        var request = new OrdersCaptureRequest(orderId);
        request.RequestBody(new OrderActionRequest());

        var response = await _client.Execute(request);
        var result = response.Result<Order>();

        return result.Status.Equals("COMPLETED", StringComparison.OrdinalIgnoreCase)
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(new Error("Payment Not Completed", $"Payment status: {result.Status}"));
    }
}