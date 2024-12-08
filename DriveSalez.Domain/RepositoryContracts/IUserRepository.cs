using DriveSalez.Domain.Aggregates.UserAggregate;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IUserRepository
{
    Task<CustomUser> AddAsync(CustomUser customUser);
    Task<CustomUser?> GetByIdAsync(Guid id);
    CustomUser Update(CustomUser customUser);
}