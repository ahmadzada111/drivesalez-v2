using Asp.Versioning;
using DriveSalez.Application.Abstractions.Payment.Factory;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.Shared.Dto.Dto.Payment;
using DriveSalez.Utilities.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.Presentation.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/payment")]
[AllowAnonymous]
public class PaymentController(
    IPayPalService payPalService,
    IPaymentService paymentService,
    IPaymentStrategyFactory paymentStrategyFactory) : Controller
{
    [HttpPost("initiate")]
    public async Task<IActionResult> InitiatePayment(PaymentInitiationRequest request)
    {
        var paymentDetails = new PaymentDetails( 
            $"{Request.Scheme}://{Request.Host}/api/v1/payment/confirm",
            $"{Request.Scheme}://{Request.Host}/api/v1/payment/cancel");

        var parseToEnum = Enum.TryParse(request.PaymentType, true, out PurchaseType purchaseType);
        if (!parseToEnum) return BadRequest();
        
        var paymentStrategy = paymentStrategyFactory.GetStrategy(purchaseType);
        var amount = await paymentStrategy.GetAmountAsync(request.ServiceId);
        var paymentResult = await payPalService.ProcessPaymentAsync(paymentDetails, amount);
        if (!paymentResult.IsSuccess) return BadRequest(paymentResult.Error!.Message);
        
        var payment = await paymentService.AddAsync(paymentResult.Value!.OrderId, amount, request.UserId, request.PaymentType);
        if (!payment.IsSuccess) return BadRequest(paymentResult.Error!.Message);

        return Ok(paymentResult.Value!);
    }
    
    [HttpGet("confirm")]
    public async Task<IActionResult> ConfirmPayment([FromQuery] string orderId, string payerId)
    {
        var result = await payPalService.CapturePaymentAsync(orderId);
        if (!result.IsSuccess) return BadRequest(result.Error!.Message);
        
        var payment = await paymentService.GetPaymentByOrderIdAsync(orderId);
        if (!payment.IsSuccess) return BadRequest(payment.Error!.Message);
        
        var modifiedPayment = await paymentService.UpdateStatusAsync(payment.Value!, PaymentStatus.Completed);
        if (!result.IsSuccess) return BadRequest(modifiedPayment.Error!.Message);
        
        return Ok(orderId);
    } 
    
    [HttpGet("cancel")]
    public async Task<IActionResult> CancelPayment([FromQuery] string orderId, string payerId)
    {
        var payment = await paymentService.GetPaymentByOrderIdAsync(orderId);
        if (!payment.IsSuccess) return BadRequest(payment.Error!.Message);
        
        var result = await paymentService.UpdateStatusAsync(payment.Value!, PaymentStatus.Voided);
        if (!result.IsSuccess) return BadRequest(result.Error!.Message);
        
        return Ok(orderId);
    } 
}