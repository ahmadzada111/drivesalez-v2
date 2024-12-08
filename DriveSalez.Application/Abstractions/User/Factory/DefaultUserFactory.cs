using DriveSalez.Application.Dto.User;
using DriveSalez.Domain.Aggregates.UserAggregate;
using DriveSalez.Domain.Common.Enums;

namespace DriveSalez.Application.Abstractions.User.Factory;

public class DefaultUserFactory : IUserFactory<SignUpDefaultAccountRequest>
{
    public CustomUser CreateUserObject(SignUpDefaultAccountRequest signUpRequest, Guid identityUserId)
    {
        var user = new CustomUser(identityUserId, signUpRequest.FirstName, signUpRequest.LastName, UserStatus.Inactive);
        return user;
    }
}