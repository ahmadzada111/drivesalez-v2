using DriveSalez.Application.Dto.Services;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Services;

internal class OneTimePurchaseService(IUnitOfWork unitOfWork) : IOneTimePurchaseService
{
    public async Task<Result<GetOneTimePurchaseResponse>> GetByIdAsync(int id)
    {
        var result = await unitOfWork.OneTimePurchaseRepository.GetByIdAsync(id);
        if (result is null) return Result<GetOneTimePurchaseResponse>.Failure(new Error(nameof(OneTimePurchase), $"Id {id} is invalid"));
        return Result<GetOneTimePurchaseResponse>.Success((GetOneTimePurchaseResponse)result);
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