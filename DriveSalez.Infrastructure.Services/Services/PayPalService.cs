using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Shared.Dto.Dto;
using DriveSalez.Utilities.Settings;
using DriveSalez.Utilities.Utilities;
using Microsoft.Extensions.Options;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;

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

    public async Task<Result<string>> ProcessPayment(PaymentDetails paymentDetails, decimal amount, string currency = "USD")
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
                UserAction = "PAY_NOW"
            },
            PurchaseUnits = new List<PurchaseUnitRequest>
            {
                new PurchaseUnitRequest
                {
                    AmountWithBreakdown = new AmountWithBreakdown
                    {
                        CurrencyCode = currency,
                        Value = amount.ToString("F2")
                    },
                    Description = "Business Account Registration Fee"
                }
            }
        };

        var request = new OrdersCreateRequest();
        request.Prefer("return=representation");
        request.RequestBody(orderRequest);

        try
        {
            var response = await _client.Execute(request);
            var result = response.Result<Order>();

            var approvalLink = result.Links.FirstOrDefault(link => link.Rel.Equals("approve", StringComparison.OrdinalIgnoreCase))?.Href;

            if (!string.IsNullOrEmpty(approvalLink))
            {
                return Result<string>.Success(approvalLink);
            }

            return Result<string>.Failure(new Error("Payment Error", "Approval URL not found."));
        }
        catch (HttpException httpEx)
        {
            var errorMessage = httpEx.Message;
            return Result<string>.Failure(new Error("Payment Error", errorMessage));
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(new Error("Payment Exception", ex.Message));
        }
    }

    public async Task<Result<bool>> CapturePayment(string orderId)
    {
        var request = new OrdersCaptureRequest(orderId);
        request.RequestBody(new OrderActionRequest());

        try
        {
            var response = await _client.Execute(request);
            var result = response.Result<Order>();

            if (result.Status.Equals("COMPLETED", StringComparison.OrdinalIgnoreCase))
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(new Error("Payment Not Completed", $"Payment status: {result.Status}"));
        }
        catch (HttpException httpEx)
        {
            var errorMessage = httpEx.Message;
            return Result<bool>.Failure(new Error("Payment Capture Error", errorMessage));
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(new Error("Payment Capture Exception", ex.Message));
        }
    }
}