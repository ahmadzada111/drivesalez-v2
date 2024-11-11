using System.Reflection;
using DriveSalez.Application.Abstractions;
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
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}