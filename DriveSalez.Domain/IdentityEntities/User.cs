using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;

namespace DriveSalez.Domain.IdentityEntities;

public class User : BaseUser
{
    public ICollection<Announcement> Announcements { get; set; } = [];
    
    public ICollection<UserLimit> UserLimits { get; set; } = [];
    
    public ICollection<Payment> Payments { get; set; } = [];

    public ICollection<OneTimePurchase> OneTimePurchases { get; set; } = [];
    
    public Subscription Subscription { get; set; } = null!;

    public UserStatus UserStatus { get; set; }
    
    public BusinessDetails? BusinessDetails { get; set; }
}