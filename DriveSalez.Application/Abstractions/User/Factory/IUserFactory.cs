using DriveSalez.Domain.Aggregates.UserAggregate;

namespace DriveSalez.Application.Abstractions.User.Factory;

public interface IUserFactory<TSignUpRequest>
{
    CustomUser CreateUserObject(TSignUpRequest signUpRequest, Guid identityUserId);
}