using DriveSalez.Application.Dto.User;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Application.Abstractions.User.Factory;

public class DefaultUserFactory : IUserFactory<SignUpDefaultAccountRequest>
{
    public Domain.IdentityEntities.User CreateUserObject(SignUpDefaultAccountRequest signUpRequest, Guid identityUserId)
    {
        var user = new Domain.IdentityEntities.User
        {
            ApplicationUserId = identityUserId,
            FirstName = signUpRequest.FirstName,
            LastName = signUpRequest.LastName,
            CreationDate = DateTimeOffset.UtcNow,
            UserStatus = UserStatus.Active,
        };
        
        return user;
    }
}