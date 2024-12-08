using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.ServiceContracts;

public interface IUserLimitService
{
    Task<Result<UserLimit>> AddLimitToUserAsync(Guid userId, int limitValue, LimitType limitType);
}