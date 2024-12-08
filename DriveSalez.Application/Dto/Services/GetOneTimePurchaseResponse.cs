using DriveSalez.Domain.Aggregates;
using DriveSalez.Domain.Aggregates.PaymentAggregate;

namespace DriveSalez.Application.Dto.Services;

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
