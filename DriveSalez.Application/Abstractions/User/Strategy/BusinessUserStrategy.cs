using DriveSalez.Application.Dto.User;
using DriveSalez.Application.ServiceContracts;

namespace DriveSalez.Application.Abstractions.User.Strategy;

public class BusinessUserStrategy(IUserService userService) : IUserStrategy<SignUpBusinessAccountRequest>
{
    public async Task<Domain.IdentityEntities.User> CreateUser(Domain.IdentityEntities.User user)
    {
        var createdUser = await userService.AddBaseUserAsync(user);
        return createdUser.Value!;
    }
}