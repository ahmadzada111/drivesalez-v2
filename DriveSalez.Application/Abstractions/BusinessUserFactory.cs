using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Shared.Dto.Dto.User;
using DriveSalez.Utilities.Utilities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Abstractions;

public class BusinessUserFactory(UserManager<ApplicationUser> userManager)
    : IUserFactory<SignUpBusinessAccountRequest>
{
    public User CreateUser(SignUpBusinessAccountRequest signUpRequest)
    {
        var user = new User
        {
            AccountBalance = 0,
            CreationDate = DateTimeOffset.UtcNow,
            BusinessDetails = new BusinessDetails
            {
                BusinessName = signUpRequest.BusinessName,
                Description = signUpRequest.Description,
                Address = signUpRequest.Address,
                // Initialize other business-specific properties
            }
        };

        return user;
    }
}