using DriveSalez.Domain.Entities;

namespace DriveSalez.Shared.Dto.Dto.Services;

public record GetOneTimePurchaseDto(int Id, string Name, decimal Price, string LimitType, int Value)
{
    public static explicit operator GetOneTimePurchaseDto(OneTimePurchase purchase)
    {
        return new GetOneTimePurchaseDto(
            purchase.Id, 
            purchase.Name, 
            purchase.Price, 
            purchase.LimitType.ToString(),
            purchase.LimitValue);
    }
}
