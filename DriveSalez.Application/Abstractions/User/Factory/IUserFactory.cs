namespace DriveSalez.Application.Abstractions.User.Factory;

public interface IUserFactory<TSignUpRequest>
{
    Domain.IdentityEntities.User CreateUserObject(TSignUpRequest signUpRequest, Guid identityUserId);
}