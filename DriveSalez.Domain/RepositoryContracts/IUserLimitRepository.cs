using DriveSalez.Domain.Aggregates.UserAggregate;

namespace DriveSalez.Domain.RepositoryContracts;

public interface IUserLimitRepository
{
    Task<UserLimit> AddAsync(UserLimit userLimit);
}