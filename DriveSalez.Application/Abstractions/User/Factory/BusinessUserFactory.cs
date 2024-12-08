using DriveSalez.Application.Dto.User;
using DriveSalez.Domain.Aggregates.UserAggregate;
using DriveSalez.Domain.Common.Enums;

namespace DriveSalez.Application.Abstractions.User.Factory;

public class BusinessUserFactory : IUserFactory<SignUpBusinessAccountRequest>
{
    public CustomUser CreateUserObject(SignUpBusinessAccountRequest signUpRequest, Guid identityUserId)
    {
        var user = new CustomUser(identityUserId, null, null, UserStatus.Inactive);
        return user;
    }
}