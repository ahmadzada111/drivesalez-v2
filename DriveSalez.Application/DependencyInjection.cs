using System.Reflection;
using DriveSalez.Application.Abstractions;
using DriveSalez.Application.Abstractions.Payment.Factory;
using DriveSalez.Application.Abstractions.Payment.Strategy;
using DriveSalez.Application.Abstractions.User;
using DriveSalez.Application.Abstractions.User.Factory;
using DriveSalez.Application.Abstractions.User.Strategy;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Application.Services;
using DriveSalez.Shared.Dto.Dto.User;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DriveSalez.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserFactory<SignUpDefaultAccountRequest>, DefaultUserFactory>();
        services.AddScoped<IUserFactory<SignUpBusinessAccountRequest>, BusinessUserFactory>();
        services.AddScoped<IUserStrategy<SignUpBusinessAccountRequest>, BusinessUserStrategy>();
        services.AddScoped<IUserStrategy<SignUpDefaultAccountRequest>, DefaultUserStrategy>();
        services.AddScoped<UserFactorySelector>();
        services.AddScoped<UserStrategySelector>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPaymentStrategy, OneTimePurchasePaymentStrategy>();
        services.AddScoped<IPaymentStrategy, SubscriptionPaymentStrategy>();
        services.AddScoped<IPaymentStrategyFactory, PaymentStrategyFactory>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<IOneTimePurchaseService, OneTimePurchaseService>();
        services.AddScoped<IUserLimitService, UserLimitService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}