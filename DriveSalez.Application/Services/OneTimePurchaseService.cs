using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using DriveSalez.Shared.Dto.Dto.Services;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Services;

internal class OneTimePurchaseService(IUnitOfWork unitOfWork) : IOneTimePurchaseService
{
    public async Task<Result<GetOneTimePurchaseRequest>> GetByIdAsync(int id)
    {
        var result = await unitOfWork.OneTimePurchaseRepository.GetByIdAsync(id);
        if (result is null) return Result<GetOneTimePurchaseRequest>.Failure(new Error(nameof(OneTimePurchase), $"Id {id} is invalid"));
        return Result<GetOneTimePurchaseRequest>.Success((GetOneTimePurchaseRequest)result);
    }

    public async Task AddOneTimePurchaseToUser(int serviceId, Guid userId)
    {
        var service = await unitOfWork.OneTimePurchaseRepository.GetByIdAsync(serviceId);
        if(service is null) throw new KeyNotFoundException("Service not found");
        
        var user = await unitOfWork.UserRepository.GetByIdAsync<Domain.IdentityEntities.User>(userId);
        if(user is null) throw new KeyNotFoundException("User not found");
        
        user.OneTimePurchases.Add(service);
        unitOfWork.UserRepository.Update(user);
        await unitOfWork.SaveChangesAsync();
    }
}