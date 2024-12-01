namespace DriveSalez.Application.Abstractions.User;

public interface IUserFactory<TSignUpRequest>
{
    Task<Domain.IdentityEntities.User> CreateUser(TSignUpRequest signUpRequest);
}