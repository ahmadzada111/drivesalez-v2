using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Shared.Dto.Dto.User;

namespace DriveSalez.Application.Abstractions.User.Strategy;

public class BusinessUserStrategy(IUserService userService) : IUserStrategy<SignUpBusinessAccountRequest>
{
    public async Task<Domain.IdentityEntities.User> CreateUser(Domain.IdentityEntities.User user)
    {
        var createdUser = await userService.AddBaseUserAsync(user);
        return createdUser.Value!;
    }
}