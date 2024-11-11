using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Shared.Dto;
using DriveSalez.Shared.Dto.Dto.Email;
using DriveSalez.Utilities.Utilities;
using FluentEmail.Core;

namespace DriveSalez.Infrastructure.Services.Services;

internal class EmailService(IFluentEmail fluentEmail) : IEmailService
{
    public async Task<Result<bool>> SendEmailAsync(EmailRequest emailRequest)
    {
       var response = await fluentEmail.To(emailRequest.ToAddress)
            .Subject(emailRequest.Subject)
            .Body(emailRequest.Body, isHtml: emailRequest.IsHtml)
            .SendAsync();

       if (!response.Successful)
       {
           var message = string.Join(" | ", response.ErrorMessages);
           return Result<bool>.Failure(new Error("Email Send Failed", message));
       }
       
       return Result<bool>.Success(true);
    }
}