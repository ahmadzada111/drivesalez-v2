using Asp.Versioning;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Shared.Dto.Dto.Payment;
using DriveSalez.Utilities.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.Presentation.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/payment")]
[AllowAnonymous]
public class PaymentController(
    IPaymentService paymentService,
    IValidator<PaymentInitiationRequest> paymentInitRequestValidator) : Controller
{
    [HttpPost("initiate")]
    public async Task<IActionResult> InitiatePayment(PaymentInitiationRequest request)
    {
        var result = await paymentInitRequestValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            var errors = string.Join("| ", result.Errors.Select(x => x.ErrorMessage));
            return Problem(errors);
        }
        
        var returnUrl = Url.Action(nameof(ConfirmPayment), "Payment", null, Request.Scheme)
            ?? throw new NullReferenceException("Return Url cannot be null");
        var cancelUrl = Url.Action(nameof(CancelPayment), "Payment", null, Request.Scheme)
            ?? throw new NullReferenceException("Cancel Url cannot be null");
        var paymentDetails = new PaymentDetails(returnUrl, cancelUrl);

        var paymentResult = await paymentService.InitiatePaymentAsync(request, paymentDetails);
        if (!paymentResult.IsSuccess) return BadRequest(paymentResult.Error!.Message);

        return Ok(paymentResult.Value!);
    }
    
    [HttpGet("confirm")]
    public async Task<IActionResult> ConfirmPayment([FromQuery] string orderId, string payerId)
    {
        if (string.IsNullOrWhiteSpace(orderId)) return BadRequest();
        var paymentStrategy = await paymentService.ConfirmPaymentAsync(orderId);
        if (!paymentStrategy.IsSuccess) return BadRequest(paymentStrategy.Error!.Message);
        return Ok(orderId);
    } 
    
    [HttpGet("cancel")]
    public async Task<IActionResult> CancelPayment([FromQuery] string orderId, string payerId)
    {
        if (string.IsNullOrWhiteSpace(orderId)) return BadRequest();
        var payment = await paymentService.GetPaymentByOrderIdAsync(orderId);
        if (!payment.IsSuccess) return BadRequest(payment.Error!.Message);

        var result = await paymentService.CancelPaymentAsync(orderId);
        if (!result.IsSuccess) return BadRequest(result.Error!.Message);
        
        return Ok(orderId);
    } 
}