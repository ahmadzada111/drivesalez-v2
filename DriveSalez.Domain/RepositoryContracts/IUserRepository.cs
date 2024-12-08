using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IUserRepository
{
    Task<TUser> AddAsync<TUser>(TUser user) where TUser : BaseUser;
    Task<TUser?> GetByIdAsync<TUser>(Guid id) where TUser : BaseUser;
    TUser Update<TUser>(TUser user) where TUser : BaseUser;
}