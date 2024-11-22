namespace DriveSalez.Application.Abstractions.User;

public interface IUserFactory<TSignUpRequest>
{
    Domain.IdentityEntities.User CreateUser(TSignUpRequest signUpRequest);
}