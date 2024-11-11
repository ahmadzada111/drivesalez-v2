using DriveSalez.Shared.Dto.Dto.Email;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IEmailService
{
    Task<Result<bool>> SendEmailAsync(EmailRequest emailMetadata);    
}