using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Application.Abstractions;

public interface IUserFactory<TSignUpRequest>
{
    User CreateUser(TSignUpRequest signUpRequest);
}