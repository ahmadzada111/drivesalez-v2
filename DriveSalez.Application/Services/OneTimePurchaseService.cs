using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Services;

internal class OneTimePurchaseService(IUnitOfWork unitOfWork) : IOneTimePurchaseService
{
    public async Task<Result<OneTimePurchase>> GetByIdAsync(int id)
    {
        var result = await unitOfWork.OneTimePurchaseRepository.GetByIdAsync(id);
        if (result is null) return Result<OneTimePurchase>.Failure(new Error(nameof(OneTimePurchase), $"Id {id} is invalid"));
        return Result<OneTimePurchase>.Success(result);
    }
}