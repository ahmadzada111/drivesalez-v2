using DriveSalez.Domain.Aggregates.PaymentAggregate;

namespace DriveSalez.Domain.Aggregates.UserAggregate;

public class UserSubscription
{
    public int Id { get; set; }
    
    public Guid CustomUserId { get; set; }
    
    public CustomUser CustomUser { get; set; } = null!;
    
    public int SubscriptionId { get; set; }
    
    public Subscription Subscription { get; set; } = null!;
    
    public DateTimeOffset ExpirationDate { get; set; }
}