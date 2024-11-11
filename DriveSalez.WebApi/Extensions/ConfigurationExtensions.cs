using DriveSalez.Utilities.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.WebApi.Extensions;

public static class ConfigurationExtensions
{
    public static void ConfigureSettings(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

        if (builder.Environment.IsDevelopment())
        {
            builder.Configuration.AddUserSecrets<Program>();
        }
        
        builder.Configuration.AddEnvironmentVariables();
        
        
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
        builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);

        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(nameof(EmailSettings)));
        builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailSettings>>().Value);

        builder.Services.Configure<RefreshTokenSettings>(builder.Configuration.GetSection(nameof(RefreshTokenSettings)));
        builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<RefreshTokenSettings>>().Value);
        
        builder.Services.Configure<BlobStorageSettings>(builder.Configuration.GetSection(nameof(BlobStorageSettings)));
        builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<BlobStorageSettings>>().Value);
        
        builder.Services.Configure<PayPalSettings>(builder.Configuration.GetSection(nameof(PayPalSettings)));
        builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<PayPalSettings>>().Value);
    }
}