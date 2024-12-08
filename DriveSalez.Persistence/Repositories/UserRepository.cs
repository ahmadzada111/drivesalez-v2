using DriveSalez.Domain.Aggregates.UserAggregate;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Persistence.Repositories;

internal class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<CustomUser> AddAsync(CustomUser customUser)
    { 
        var entry = await context.CustomUsers.AddAsync(customUser);
        return entry.Entity;
    }
    
    public async Task<CustomUser?> GetByIdAsync(Guid id)
    {
        return await context.CustomUsers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public CustomUser Update(CustomUser customUser)
    {
        return context.CustomUsers.Update(customUser).Entity;
    }
}