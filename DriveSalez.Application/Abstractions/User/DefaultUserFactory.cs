using DriveSalez.Domain.Enums;
using DriveSalez.Shared.Dto.Dto.User;

namespace DriveSalez.Application.Abstractions.User;

public class DefaultUserFactory : IUserFactory<SignUpDefaultAccountRequest>
{
    public Domain.IdentityEntities.User CreateUser(SignUpDefaultAccountRequest signUpRequest)
    {
        var user = new Domain.IdentityEntities.User
        {
            FirstName = signUpRequest.FirstName,
            LastName = signUpRequest.LastName,
            CreationDate = DateTimeOffset.UtcNow,
            UserStatus = UserStatus.Active
        };
        
        return user;
    }
}