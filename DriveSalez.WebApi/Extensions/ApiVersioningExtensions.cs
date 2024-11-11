using Asp.Versioning;

namespace DriveSalez.WebApi.Extensions;

public static class ApiVersioningExtensions
{
    public static IServiceCollection AddApiVersioningWithExplorer(this IServiceCollection services)
    {
        services.AddApiVersioning(e =>
        {
            e.AssumeDefaultVersionWhenUnspecified = true;
            e.DefaultApiVersion = new ApiVersion(1);
            e.ReportApiVersions = true;
            e.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(e =>
        {
            e.GroupNameFormat = "'v'V";
            e.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}