using DriveSalez.Domain.Enums;

namespace DriveSalez.Domain.Entities;

public class Subscription : PaidService
{
    public DateTimeOffset ExpirationDate { get; set; }
    
    public UserType UserType { get; set; }

    public ICollection<SubscriptionLimit> SubscriptionLimits { get; set; } = [];
}