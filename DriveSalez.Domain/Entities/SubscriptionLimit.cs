using DriveSalez.Domain.Enums;

namespace DriveSalez.Domain.Entities;

public class SubscriptionLimit
{
    public int Id { get; set; }
    
    public int SubscriptionId { get; set; }
    
    public required Subscription Subscription { get; set; }
    
    public LimitType LimitType { get; set; }
    
    public int LimitValue { get; set; }
}