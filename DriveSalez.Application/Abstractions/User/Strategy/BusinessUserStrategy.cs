using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using DriveSalez.Shared.Dto.Dto.User;

namespace DriveSalez.Application.Abstractions.User.Strategy;

public class BusinessUserStrategy(IUnitOfWork unitOfWork, IUserService userService) : IUserStrategy<SignUpBusinessAccountRequest>
{
    public async Task<Domain.IdentityEntities.User> CreateUser(Domain.IdentityEntities.User user)
    {
        var createdUser = await userService.AddBaseUserAsync(user);
        return createdUser.Value!;
    }
}