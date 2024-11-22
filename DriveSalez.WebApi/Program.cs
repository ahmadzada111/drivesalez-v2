using DriveSalez.Application;
using DriveSalez.Infrastructure.Services;
using DriveSalez.Persistence;
using DriveSalez.Presentation;
using DriveSalez.WebApi.ExceptionHandler;
using DriveSalez.WebApi.Extensions;

namespace DriveSalez.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureSettings();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger();
        builder.Services.AddApiVersioningWithExplorer();
        builder.Services.AddIdentityServices(builder.Configuration);
        builder.Services.AddAuthenticationWithJwt(builder.Configuration);
        builder.Services.AddAuthorizationPolicies();
        builder.Services.AddHttpClient();
        builder.Services.AddCorsPolicy();
        builder.Services.AddLogging();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddApplicationLayer();
        builder.Services.AddPresentationLayer();
        builder.Services.AddInfrastructureServiceLayer(builder.Configuration);
        builder.Services.AddPersistenceLayer(builder.Configuration);
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(x => x.EnablePersistAuthorization());
        }

        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseCors("DriveSalezCorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}