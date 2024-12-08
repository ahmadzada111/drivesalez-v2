using DriveSalez.Application.Dto.Email;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.ServiceContracts;

public interface IEmailService
{
    Task<Result<bool>> SendEmailAsync(EmailRequest emailMetadata);    
}