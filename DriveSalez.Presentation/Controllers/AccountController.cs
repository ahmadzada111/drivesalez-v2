using Asp.Versioning;
using DriveSalez.Application.Dto.Email;
using DriveSalez.Application.Dto.User;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Common.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.Presentation.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/accounts")]
public class AccountController(
    IUserService userService, 
    IIdentityService identityService,
    IValidator<SignUpDefaultAccountRequest> signUpDefaultValidator,
    IValidator<SignUpBusinessAccountRequest> signUpBusinessValidator,
    IValidator<ConfirmEmailRequest> emailConfirmValidator,
    IEmailService emailService) : Controller
{
    [HttpPost("signup/default"), AllowAnonymous]
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
        
        return CreatedAtAction(nameof(SignUpDefaultAccount), result.Value);
    }
    
    [HttpPost("signup/business"), AllowAnonymous]
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
        
        return CreatedAtAction(nameof(SignUpBusinessAccount), result.Value);
    }
    
    [HttpPatch("signup/complete"), AllowAnonymous]
    public async Task<ActionResult> CompleteBusinessAccountSignUp([FromBody] Guid pendingUserId, string orderId)
    {
        if (string.IsNullOrWhiteSpace(orderId) || pendingUserId == Guid.Empty) return BadRequest();
        await userService.CompleteBusinessSignUpAsync(pendingUserId, orderId);
        return Ok();
    }
    
    [HttpPost("signin")]
    public Task<ActionResult> SignIn()
    {
        throw new NotImplementedException();
    }

    [HttpPost("{userId}/profile")]
    public Task<ActionResult> Profile()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("signout")]
    public async Task<ActionResult> SignOutAsync()
    {
        await identityService.SignOutAsync();
        return NoContent();
    }

    [HttpGet("{userId}/email")]
    public async Task<ActionResult> RequestConfirmEmail([FromRoute] Guid userId)
    {
        if (userId == Guid.Empty) return BadRequest();
        var user = await identityService.FindIdentityUserByIdAsync(userId);
        if (!user.IsSuccess) return BadRequest(user.Error);
        
        var token = await identityService.GenerateEmailConfirmationTokenAsync(user.Value!);
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
        if (userId == Guid.Empty || string.IsNullOrWhiteSpace(token)) return BadRequest();
        var request = new ConfirmEmailRequest(userId, token);
        var validationResult = await emailConfirmValidator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            string errorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Problem(errorMessage);
        }

        var user = await identityService.FindIdentityUserByIdAsync(userId);
        if(!user.IsSuccess) return BadRequest(user.Error);
        
        var result = await identityService.ConfirmEmailAsync(user.Value!, request.Token);
        if (result.IsSuccess) return Ok("Email confirmed successfully.");
        
        return BadRequest(result.Error);
    }
}