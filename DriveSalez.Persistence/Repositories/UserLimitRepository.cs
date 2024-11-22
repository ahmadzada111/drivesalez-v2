using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.DbContext;
using DriveSalez.Repository.Contracts.RepositoryContracts;

namespace DriveSalez.Persistence.Repositories;

internal class UserLimitRepository(ApplicationDbContext context) : IUserLimitRepository
{
    public async Task<UserLimit> AddAsync(UserLimit userLimit)
    {
        await context.UserLimits.AddAsync(userLimit);
        return userLimit;
    }
}