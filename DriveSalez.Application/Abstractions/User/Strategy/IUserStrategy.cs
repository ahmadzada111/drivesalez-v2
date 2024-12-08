using DriveSalez.Domain.Aggregates.UserAggregate;

namespace DriveSalez.Application.Abstractions.User.Strategy;

public interface IUserStrategy<TSignUpRequest>
{
    Task<CustomUser> CreateUser(CustomUser customUser);
}