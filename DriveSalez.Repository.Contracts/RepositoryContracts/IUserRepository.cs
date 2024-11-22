using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Repository.Contracts.RepositoryContracts;

public interface IUserRepository
{
    Task<TUser> AddAsync<TUser>(TUser user) where TUser : BaseUser;
    Task<TUser?> GetByIdAsync<TUser>(Guid id) where TUser : BaseUser;
}