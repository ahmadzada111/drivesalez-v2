using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class UserSubscription
{
    public int Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; } = null!;

    public int SubscriptionId { get; set; }
    
    public Subscription Subscription { get; set; } = null!;
    
    public DateTimeOffset ExpirationDate { get; set; }
}