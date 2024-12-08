using DriveSalez.Domain.Entities;

namespace DriveSalez.Application.Dto.Services;

public record GetSubscriptionResponse(
    int Id,
    string Name,
    decimal Price,
    int ValidForDays,
    ICollection<SubscriptionLimitResponse> SubscriptionLimits)
{
    public static explicit operator GetSubscriptionResponse(Subscription subscription)
    {
        return new GetSubscriptionResponse(
            subscription.Id,
            subscription.Name,
            subscription.Price,
            subscription.ValidForDays,
            subscription.SubscriptionLimits
                .Select(limit => (SubscriptionLimitResponse)limit)
                .ToList());
    }
}