using DriveSalez.Shared.Dto.Dto.User;
using Microsoft.Extensions.DependencyInjection;

namespace DriveSalez.Application.Abstractions;

public class UserFactorySelector(IServiceProvider serviceProvider)
{
    public IUserFactory<TSignUpRequest> GetFactory<TSignUpRequest>()
    {
        var factory = serviceProvider.GetService<IUserFactory<TSignUpRequest>>();
        if (factory == null)
        {
            throw new NotImplementedException($"No factory implemented for sign-up request type {typeof(TSignUpRequest).Name}");
        }
        return factory;
    }
}