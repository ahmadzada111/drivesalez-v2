using Asp.Versioning;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Shared.Dto.Dto.Email;
using DriveSalez.Shared.Dto.Dto.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.Presentation.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/accounts")]
[AllowAnonymous]
public class AccountController(
    IUserService userService, 
    IValidator<SignUpDefaultAccountRequest> signUpDefaultValidator,
    IValidator<SignUpBusinessAccountRequest> signUpBusinessValidator,
    IValidator<ConfirmEmailRequest> emailConfirmValidator,
    IEmailService emailService,
    IPaymentService paymentService) : Controller
{
    [HttpPost("signup/default")]
    public async Task<ActionResult> SignUpDefaultAccount([FromBody] SignUpDefaultAccountRequest request)
    {
        var validator = await signUpDefaultValidator.ValidateAsync(request);
        if (!validator.IsValid)
        {
            var message = string.Join(" | ", validator.Errors.Select(x => x.ErrorMessage));
            return Problem(message);
        }

        var result = await userService.CreateUserAsync(request, UserType.Default);
        if (!result.IsSuccess) return BadRequest(result.Error);
        
        return CreatedAtAction(nameof(SignUpDefaultAccount), "User registered successfully.");
    }
    
    [HttpPost("signup/business")]
    public async Task<ActionResult> SignUpBusinessAccount([FromBody] SignUpBusinessAccountRequest request)
    {
        var validator = await signUpBusinessValidator.ValidateAsync(request);
        if (!validator.IsValid)
        {
            var message = string.Join(" | ", validator.Errors.Select(x => x.ErrorMessage));
            return Problem(message);
        }

        var result = await userService.CreateUserAsync(request, UserType.Business);
        if (!result.IsSuccess) return BadRequest(result.Error);
        
        return CreatedAtAction(nameof(SignUpBusinessAccount), "User registered successfully.");
    }
    
    [HttpPost("signup/complete")]
    public async Task<ActionResult> CompleteBusinessAccountSignUp([FromBody] Guid pendingUserId, string orderId)
    {
        var user = await userService.FindBaseUserByIdAsync<User>(pendingUserId);
        if (!user.IsSuccess) return BadRequest();
        
        var payment = await paymentService.GetPaymentByOrderIdAsync(orderId);
        if (!payment.IsSuccess) return BadRequest();

        if (user.Value!.Id != payment.Value!.UserId
            || payment.Value!.PurchaseType != PurchaseType.Subscription
            || payment.Value!.PaymentStatus != PaymentStatus.Completed) return BadRequest();
        
        user.Value!.UserStatus = UserStatus.Active;
        await userService.UpdateBaseUserAsync(user.Value!);
        
        return Ok();
    }
    
    [HttpPost("signin")]
    public Task<ActionResult> SignIn()
    {
        throw new NotImplementedException();
    }

    [HttpPut("logout")]
    public async Task<ActionResult> LogOut()
    {
        await userService.LogOutAsync();
        return NoContent();
    }

    [HttpGet("{userId}/email")]
    public async Task<ActionResult> RequestConfirmEmail([FromRoute] Guid userId)
    {
        var user = await userService.FindIdentityUserByIdAsync(userId);
        if (!user.IsSuccess) return BadRequest(user.Error);
        
        var token = await userService.GenerateEmailConfirmationTokenAsync(user.Value!);
        if (!token.IsSuccess) return BadRequest(token.Error);
        
        var confirmationLink = Url.Action(
            nameof(ConfirmEmail), 
            "Account", 
            new { userId = user.Value!.Id, token }, 
            Request.Scheme);

        var emailResult = await emailService.SendEmailAsync(new EmailRequest
        (
            user.Value.Email!,
            "Email Confirmation",
            $"Please confirm your account by clicking this link: {confirmationLink}"
        ));
        if (!emailResult.IsSuccess) return BadRequest(emailResult.Error);
        
        return Ok("Email confirmation link has been sent.");
    }

    [HttpPut("{userId}/email")]
    public async Task<ActionResult> ConfirmEmail([FromRoute] Guid userId, [FromQuery] string token)
    {
        var request = new ConfirmEmailRequest(userId, token);
        var validationResult = await emailConfirmValidator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            string errorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        var user = await userService.FindIdentityUserByIdAsync(userId);
        if(!user.IsSuccess) return BadRequest(user.Error);
        
        var result = await userService.ConfirmEmailAsync(user.Value!, request.Token);
        if (result.IsSuccess) return Ok("Email confirmed successfully.");
        
        return BadRequest(result.Error);
    }
}