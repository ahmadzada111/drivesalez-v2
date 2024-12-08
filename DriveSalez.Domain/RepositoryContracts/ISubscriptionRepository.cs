using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Domain.RepositoryContracts;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(int id);
    Task<Subscription?> GetByUserTypeAsync(UserType userType);
}