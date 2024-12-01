using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.Shared.Dto.Dto.User;

namespace DriveSalez.Application.Abstractions.User.Strategy;

public class DefaultUserStrategy(
    ISubscriptionService subscriptionService,
    IUserService userService,
    IUserLimitService userLimitService) : IUserStrategy<SignUpDefaultAccountRequest>
{
    public async Task<Domain.IdentityEntities.User> CreateUser(Domain.IdentityEntities.User user)
    {
        var subscription = await subscriptionService.GetByUserTypeAsync(UserType.Default);
        if (!subscription.IsSuccess) throw new KeyNotFoundException("Subscription not found");

        var premiumLimit = subscription.Value!.SubscriptionLimits.FirstOrDefault(x => x.LimitType == LimitType.Premium)
            ?? throw new KeyNotFoundException("Premium limit not found");
        var regularLimit = subscription.Value!.SubscriptionLimits.FirstOrDefault(x => x.LimitType == LimitType.Regular)
            ?? throw new KeyNotFoundException("Regular limit not found");
        
        await userService.AddBaseUserAsync(user);
        await userLimitService.AddLimitToUserAsync(user.Id,  premiumLimit.LimitValue, premiumLimit.LimitType);
        await userLimitService.AddLimitToUserAsync(user.Id,  regularLimit.LimitValue, regularLimit.LimitType);
        
        return user;
    }
}