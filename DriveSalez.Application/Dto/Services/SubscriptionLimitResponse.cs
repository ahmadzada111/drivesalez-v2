using DriveSalez.Domain.Aggregates;
using DriveSalez.Domain.Aggregates.PaymentAggregate;

namespace DriveSalez.Application.Dto.Services;

public record SubscriptionLimitResponse(
    int Id,
    int SubscriptionId,
    string LimitType,
    int LimitValue)
{
    public static explicit operator SubscriptionLimitResponse(SubscriptionLimit limit)
    {
        return new SubscriptionLimitResponse(
            limit.Id, 
            limit.SubscriptionId,
            limit.LimitType.ToString(), 
            limit.LimitValue);
    }
}