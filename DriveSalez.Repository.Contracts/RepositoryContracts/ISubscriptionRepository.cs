using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Repository.Contracts.RepositoryContracts;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(int id);
    Task<Subscription?> GetByUserTypeAsync(UserType userType);
}