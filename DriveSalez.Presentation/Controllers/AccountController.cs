using Asp.Versioning;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.Shared.Dto.Dto.Email;
using DriveSalez.Shared.Dto.Dto.User;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.Presentation.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/accounts")]
[ServiceFilter(typeof(LoggingFilter))]
public class AccountController(
    IUserService userService, 
    IValidator<SignUpDefaultAccountRequest> signUpDefaultValidator,
    IValidator<SignUpBusinessAccountRequest> signUpBusinessValidator,
    IValidator<ConfirmEmailRequest> emailConfirmValidator,
    IEmailService emailService) : Controller
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

        var result = await userService.CreateUser(request, UserType.Default);
        if (!result.IsSuccess) return BadRequest(result.Error);

        var user = await userService.FindByEmail(request.Email);
        if (user.Value is null) return BadRequest(user.Error);
        
        var token = await userService.GenerateEmailConfirmationToken(user.Value);
        var confirmationLink = Url.Action(
            nameof(ConfirmEmail), 
            "Account", 
            new { userId = user.Value.Id, token }, 
            Request.Scheme);

        await emailService.SendEmailAsync(new EmailRequest
        (
            user.Value.Email!,
            "Email Confirmation",
            $"Please confirm your account by clicking this link: {confirmationLink}"
        ));
        
        return CreatedAtAction(nameof(SignUpDefaultAccount), new { userId = user.Value.Id }, "User registered successfully.");
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

        return Ok();
    }
    
    [HttpPost("signup")]
    public Task<ActionResult> SignIn()
    {
        throw new NotImplementedException();
    }

    [HttpPut("logout")]
    public async Task<ActionResult> LogOut()
    {
        await userService.LogOut();
        return NoContent();
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

        var user = await userService.FindById(userId);
        if(user.Value is null) return BadRequest(user.Error);
        
        var result = await userService.ConfirmEmail(user.Value, request.Token);
        
        if (result.IsSuccess)
        {
            return Ok("Email confirmed successfully.");
        }

        return BadRequest(result.Error);
    }
}