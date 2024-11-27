using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Repository.Contracts.RepositoryContracts;
using DriveSalez.Shared.Dto.Dto.Services;
using DriveSalez.Utilities.Utilities;

namespace DriveSalez.Application.Services;

internal class OneTimePurchaseService(IUnitOfWork unitOfWork) : IOneTimePurchaseService
{
    public async Task<Result<GetOneTimePurchaseDto>> GetByIdAsync(int id)
    {
        var result = await unitOfWork.OneTimePurchaseRepository.GetByIdAsync(id);
        if (result is null) return Result<GetOneTimePurchaseDto>.Failure(new Error(nameof(OneTimePurchase), $"Id {id} is invalid"));
        return Result<GetOneTimePurchaseDto>.Success((GetOneTimePurchaseDto)result);
    }
}