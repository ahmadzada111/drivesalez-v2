using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Persistence.Repositories;

internal class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<TUser> AddAsync<TUser>(TUser user) where TUser : BaseUser
    { 
        var entry = await context.Set<TUser>().AddAsync(user);
        return entry.Entity;
    }
    
    public async Task<TUser?> GetByIdAsync<TUser>(Guid id) where TUser : BaseUser
    {
        return await context.Set<TUser>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public TUser Update<TUser>(TUser user) where TUser : BaseUser
    {
        return context.Set<TUser>().Update(user).Entity;
    }
}