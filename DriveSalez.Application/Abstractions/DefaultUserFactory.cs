using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Shared.Dto.Dto.User;

namespace DriveSalez.Application.Abstractions;

public class DefaultUserFactory : IUserFactory<SignUpDefaultAccountRequest>
{
    public User CreateUser(SignUpDefaultAccountRequest signUpRequest)
    {
        var user = new User
        {
            FirstName = signUpRequest.FirstName,
            LastName = signUpRequest.LastName,
            AccountBalance = 0,
            CreationDate = DateTimeOffset.UtcNow,
        };

        return user;
    }
}