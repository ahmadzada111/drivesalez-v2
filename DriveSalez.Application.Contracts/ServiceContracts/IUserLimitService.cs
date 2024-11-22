using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IUserLimitService
{
    Task<Result<UserLimit>> AddUserLimitToUser(Guid userId, int limitValue, LimitType limitType);
}