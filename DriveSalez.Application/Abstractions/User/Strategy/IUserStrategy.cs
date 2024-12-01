namespace DriveSalez.Application.Abstractions.User.Strategy;

public interface IUserStrategy<TSignUpRequest>
{
    Task<Domain.IdentityEntities.User> CreateUser(Domain.IdentityEntities.User user);
}