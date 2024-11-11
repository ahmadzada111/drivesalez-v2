using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Infrastructure.Services.Services;
using DriveSalez.Utilities.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DriveSalez.Infrastructure.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServiceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPayPalService, PayPalService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IBlobStorageService, BlobStorageService>();

        var emailSettings = configuration.GetSection(nameof(EmailSettings)).Get<EmailSettings>() 
                            ?? throw new InvalidOperationException($"{nameof(EmailSettings)} cannot be null");

        services.AddFluentEmail(emailSettings.CompanyEmail)
            .AddSmtpSender(emailSettings.SmtpServer, 
                emailSettings.Port, 
                emailSettings.CompanyEmail, 
                emailSettings.EmailKey);

        return services;
    }
}