using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.Shared.Dto.Dto.Services;

namespace DriveSalez.Application.Abstractions.Payment.Strategy;

public class OneTimePurchasePaymentStrategy(IOneTimePurchaseService oneTimePurchaseService) : IPaymentStrategy
{
    public PurchaseType PaymentType => PurchaseType.OneTimePurchase;
    
    public async Task<GetServiceRequest> GetService(int serviceId)
    {
        var result = await oneTimePurchaseService.GetByIdAsync(serviceId);
        if (result.IsSuccess) return new GetServiceRequest(result.Value!.Id, result.Value!.Name, result.Value!.Price);
        throw new KeyNotFoundException("Service not found");
    }

    public async Task HandlePostPaymentAsync(int serviceId, Guid userId)
    {
        await oneTimePurchaseService.AddOneTimePurchaseToUser(serviceId, userId);
    }
}