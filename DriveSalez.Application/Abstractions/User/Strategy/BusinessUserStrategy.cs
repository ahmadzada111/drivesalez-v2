using DriveSalez.Application.Dto.User;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Aggregates.UserAggregate;

namespace DriveSalez.Application.Abstractions.User.Strategy;

public class BusinessUserStrategy(IUserService userService) : IUserStrategy<SignUpBusinessAccountRequest>
{
    public async Task<CustomUser> CreateUser(CustomUser customUser)
    {
        var createdUser = await userService.AddCustomUserAsync(customUser);
        return createdUser.Value!;
    }
}