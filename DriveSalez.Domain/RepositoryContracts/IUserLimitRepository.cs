using DriveSalez.Domain.Entities;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IUserLimitRepository
{
    Task<UserLimit> AddAsync(UserLimit userLimit);
}