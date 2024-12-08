using DriveSalez.Application.Dto.User;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Aggregates.UserAggregate;
using DriveSalez.Domain.Common.Enums;

namespace DriveSalez.Application.Abstractions.User.Strategy;

public class DefaultUserStrategy(
    ISubscriptionService subscriptionService,
    IUserService userService,
    IUserLimitService userLimitService) : IUserStrategy<SignUpDefaultAccountRequest>
{
    public async Task<CustomUser> CreateUser(CustomUser customUser)
    {
        var subscription = await subscriptionService.GetByUserTypeAsync(UserType.Default);
        if (!subscription.IsSuccess) throw new KeyNotFoundException("Subscription not found");

        var premiumLimit = subscription.Value!.SubscriptionLimits.FirstOrDefault(x => x.LimitType == LimitType.Premium.ToString())
            ?? throw new KeyNotFoundException("Premium limit not found");
        var regularLimit = subscription.Value!.SubscriptionLimits.FirstOrDefault(x => x.LimitType == LimitType.Regular.ToString())
            ?? throw new KeyNotFoundException("Regular limit not found");
        
        await userService.AddCustomUserAsync(customUser);
        await userLimitService.AddLimitToUserAsync(customUser.Id,  premiumLimit.LimitValue, LimitType.Premium);
        await userLimitService.AddLimitToUserAsync(customUser.Id,  regularLimit.LimitValue, LimitType.Regular);
        await subscriptionService.AddSubscriptionToUser(subscription.Value!.Id, customUser.Id);
        return customUser;
    }
}