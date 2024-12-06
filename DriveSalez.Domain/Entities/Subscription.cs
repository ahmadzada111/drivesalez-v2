using DriveSalez.Domain.Enums;

namespace DriveSalez.Domain.Entities;

public class Subscription : PaidService
{
    public int ValidForDays { get; set; }
    
    public UserType UserType { get; set; }

    public ICollection<UserSubscription> UserSubscriptions { get; set; } = [];

    public ICollection<SubscriptionLimit> SubscriptionLimits { get; set; } = [];
}