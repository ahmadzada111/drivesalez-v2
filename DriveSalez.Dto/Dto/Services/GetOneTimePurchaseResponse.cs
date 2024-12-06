using DriveSalez.Domain.Entities;

namespace DriveSalez.Shared.Dto.Dto.Services;

public record GetOneTimePurchaseResponse(
    int Id, 
    string Name, 
    decimal Price, 
    string LimitType, 
    int Value)
{
    public static explicit operator GetOneTimePurchaseResponse(OneTimePurchase purchase)
    {
        return new GetOneTimePurchaseResponse(
            purchase.Id, 
            purchase.Name, 
            purchase.Price, 
            purchase.LimitType.ToString(),
            purchase.LimitValue);
    }
}
