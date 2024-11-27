using DriveSalez.Domain.Entities;

namespace DriveSalez.Shared.Dto.Dto.Services;

public record GetOneTimePurchaseRequest(int Id, string Name, decimal Price, string LimitType, int Value)
{
    public static explicit operator GetOneTimePurchaseRequest(OneTimePurchase purchase)
    {
        return new GetOneTimePurchaseRequest(
            purchase.Id, 
            purchase.Name, 
            purchase.Price, 
            purchase.LimitType.ToString(),
            purchase.LimitValue);
    }
}
