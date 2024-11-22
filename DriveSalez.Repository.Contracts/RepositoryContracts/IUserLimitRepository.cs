using DriveSalez.Domain.Entities;

namespace DriveSalez.Repository.Contracts.RepositoryContracts;

public interface IUserLimitRepository
{
    Task<UserLimit> AddAsync(UserLimit userLimit);
}