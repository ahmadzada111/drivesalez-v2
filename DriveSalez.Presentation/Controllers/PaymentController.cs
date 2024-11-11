using Asp.Versioning;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Shared.Dto.Dto;
using DriveSalez.Utilities.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.Presentation.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/accounts")]
[ServiceFilter(typeof(LoggingFilter))]
public class PaymentController(IPayPalService payPalService) : Controller
{
    // [HttpPost("business/initiate")]
    // public async Task<IActionResult> InitiateBusinessPayment()
    // {
    //     var paymentDetails = new PaymentDetails( 
    //         $"{Request.Scheme}://{Request.Host}/api/payment/business/confirm",
    //         $"{Request.Scheme}://{Request.Host}/payment/cancelled");
    //
    //     var amount = 99.99m;
    //     var paymentResult = await payPalService.InitiatePayment(paymentDetails, amount);
    //
    //     if (!paymentResult.IsSuccess)
    //     {
    //         return BadRequest(paymentResult.Error.Message);
    //     }
    //
    //     return Redirect(paymentResult.Value);
    // }
}