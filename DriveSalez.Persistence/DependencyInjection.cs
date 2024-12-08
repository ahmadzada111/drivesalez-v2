using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using DriveSalez.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DriveSalez.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IOneTimePurchaseRepository, OneTimePurchaseRepository>();
        services.AddScoped<IUserLimitRepository, UserLimitRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(Lazy<>), typeof(LazyResolver<>));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection")));

        return services;
    }
}

file class LazyResolver<T>(IServiceProvider provider) : Lazy<T>(provider.GetRequiredService<T>) where T : notnull;