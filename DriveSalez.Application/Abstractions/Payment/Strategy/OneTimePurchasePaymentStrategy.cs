using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Application.Abstractions.Payment.Strategy;

public class OneTimePurchasePaymentStrategy(
    IOneTimePurchaseService oneTimePurchaseService, 
    IUserService userService) : IPaymentStrategy
{
    public PurchaseType PaymentType => PurchaseType.OneTimePurchase;
    
    public async Task<decimal> GetAmountAsync(int serviceId)
    {
        var result = await oneTimePurchaseService.GetByIdAsync(serviceId);
        if (result.IsSuccess) return result.Value!.Price;
        throw new KeyNotFoundException();
    }

    public async Task HandlePostPaymentAsync(int serviceId, Guid userId)
    {
        var service = await oneTimePurchaseService.GetByIdAsync(serviceId);
        if(!service.IsSuccess) throw new KeyNotFoundException();
        
    }
}