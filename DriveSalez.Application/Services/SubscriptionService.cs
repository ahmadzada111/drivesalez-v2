using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using DriveSalez.Shared.Dto.Dto.Services;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Services;

internal class SubscriptionService(IUnitOfWork unitOfWork) : ISubscriptionService
{
    public async Task<Result<GetSubscriptionResponse>> GetByIdAsync(int id)
    {
        var result = await unitOfWork.SubscriptionRepository.GetByIdAsync(id);
        if (result is null) return Result<GetSubscriptionResponse>.Failure(new Error("Subscription not found", "Subscription not found"));
        return Result<GetSubscriptionResponse>.Success((GetSubscriptionResponse)result);
    }
    
    public async Task<Result<GetSubscriptionResponse>> GetByUserTypeAsync(UserType userType)
    {
        var result = await unitOfWork.SubscriptionRepository.GetByUserTypeAsync(userType);
        if (result is null) return Result<GetSubscriptionResponse>.Failure(new Error("Subscription not found", "Subscription not found"));
        return Result<GetSubscriptionResponse>.Success((GetSubscriptionResponse)result);
    }

    public async Task AddSubscriptionToUser(int serviceId, Guid baseUserId)
    {
        var service = await unitOfWork.SubscriptionRepository.GetByIdAsync(serviceId);
        if(service is null) throw new KeyNotFoundException("Service not found");
        
        var user = await unitOfWork.UserRepository.GetByIdAsync<Domain.IdentityEntities.User>(baseUserId);
        if(user is null) throw new KeyNotFoundException("User not found");

        var userSubscription = new UserSubscription()
        {
            UserId = user.Id,
            SubscriptionId = service.Id,
            ExpirationDate = DateTimeOffset.UtcNow.AddDays(service.ValidForDays)
        };
        await unitOfWork.UserSubscriptionRepository.AddAsync(userSubscription);
        await unitOfWork.SaveChangesAsync();
    }
}