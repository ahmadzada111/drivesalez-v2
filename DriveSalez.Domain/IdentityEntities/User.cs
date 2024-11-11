using DriveSalez.Domain.Entities;

namespace DriveSalez.Domain.IdentityEntities;

public class User : BaseUser
{
    public ICollection<Announcement> Announcements { get; set; } = [];
    
    // public ICollection<UserLimit> UserLimits { get; set; } = [];
    
    // public ICollection<UserPurchase> UserPurchases { get; set; } = [];
    
    // public UserSubscription Subscription { get; }
    
    public decimal AccountBalance { get; set; }
    
    public BusinessDetails? BusinessDetails { get; set; }
}