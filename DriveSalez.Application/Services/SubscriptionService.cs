using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Services;

internal class SubscriptionService(IUnitOfWork unitOfWork) : ISubscriptionService
{
    public async Task<Result<Subscription>> GetByIdAsync(int id)
    {
        var result = await unitOfWork.SubscriptionRepository.GetByIdAsync(id);
        if (result is null) return Result<Subscription>.Failure(new Error("Subscription not found", "Subscription not found"));
        return Result<Subscription>.Success(result);
    }
    
    public async Task<Result<Subscription>> GetByUserTypeAsync(UserType userType)
    {
        var result = await unitOfWork.SubscriptionRepository.GetByUserTypeAsync(userType);
        if (result is null) return Result<Subscription>.Failure(new Error("Subscription not found", "Subscription not found"));
        return Result<Subscription>.Success(result);
    }

    public async Task AddSubscriptionToUser(int serviceId, Guid userId)
    {
        var service = await unitOfWork.SubscriptionRepository.GetByIdAsync(serviceId);
        if(service is null) throw new KeyNotFoundException("Service not found");
        
        var user = await unitOfWork.UserRepository.GetByIdAsync<Domain.IdentityEntities.User>(userId);
        if(user is null) throw new KeyNotFoundException("User not found");
        
        user.Subscription = service;
        unitOfWork.UserRepository.Update(user);
        await unitOfWork.SaveChangesAsync();
    }
}