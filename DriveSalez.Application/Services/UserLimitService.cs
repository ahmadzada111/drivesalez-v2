using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Services;

internal class UserLimitService(IUnitOfWork unitOfWork) : IUserLimitService
{
    public async Task<Result<UserLimit>> AddUserLimitToUser(Guid userId, int limitValue, LimitType limitType)
    {
        var userLimit = new UserLimit()
        {
            UserId = userId,
            LimitType = limitType,
            LimitValue = limitValue,
            UsedValue = 0
        };

        var result = await unitOfWork.UserLimitRepository.AddAsync(userLimit);
        await unitOfWork.SaveChangesAsync();
        return Result<UserLimit>.Success(result);
    }
}