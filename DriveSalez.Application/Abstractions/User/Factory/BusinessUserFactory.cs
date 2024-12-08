using DriveSalez.Application.Dto.User;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Application.Abstractions.User.Factory;

public class BusinessUserFactory : IUserFactory<SignUpBusinessAccountRequest>
{
    public Domain.IdentityEntities.User CreateUserObject(SignUpBusinessAccountRequest signUpRequest, Guid identityUserId)
    {
        var user = new Domain.IdentityEntities.User
        {
            ApplicationUserId = identityUserId,
            CreationDate = DateTimeOffset.UtcNow,
            UserStatus = UserStatus.Inactive,
            BusinessDetails = new BusinessDetails
            {
                BusinessName = signUpRequest.BusinessName,
                Description = signUpRequest.Description,
                Address = signUpRequest.Address,
            }
        };
        
        return user;
    }
}