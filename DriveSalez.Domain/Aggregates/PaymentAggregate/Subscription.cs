using DriveSalez.Domain.Aggregates.UserAggregate;
using DriveSalez.Domain.Common.Enums;

namespace DriveSalez.Domain.Aggregates.PaymentAggregate;

public class Subscription : PaidService
{
    public int ValidForDays { get; set; }
    
    public UserType UserType { get; set; }

    public ICollection<UserSubscription> UserSubscriptions { get; set; } = [];

    public ICollection<SubscriptionLimit> SubscriptionLimits { get; set; } = [];
    
    public Subscription(int validForDays, UserType userType, string name, decimal price) : base(name, price)
    {
        ValidForDays = validForDays;
        UserType = userType;
    }
}