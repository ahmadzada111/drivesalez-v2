using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using DriveSalez.Presentation.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace DriveSalez.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
    {
        services.AddScoped<AuthorizedUserFilter>();
        
        services.AddControllers(options =>
            {
                // options.Filters.Add<AuthorizedUserFilter>();
                options.Filters.Add<LoggingFilter>();
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddApplicationPart(Assembly.GetExecutingAssembly());

        return services;
    }
}