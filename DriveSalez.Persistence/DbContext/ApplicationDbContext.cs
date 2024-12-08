using DriveSalez.Domain.Aggregates;
using DriveSalez.Domain.Aggregates.PaymentAggregate;
using DriveSalez.Domain.Aggregates.UserAggregate;
using DriveSalez.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Persistence.DbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public virtual DbSet<CustomUser> CustomUsers { get; set; }
    
    public virtual DbSet<Image> ImageUrls { get; set; }
    
    public virtual DbSet<WorkHour> WorkHours { get; set; }
    
    public virtual DbSet<Announcement> Announcements { get; set; }
    
    public virtual DbSet<Payment> Payments { get; set; }
 
    public virtual DbSet<Subscription> Subscriptions { get; set; }
    
    public virtual DbSet<UserLimit> UserLimits { get; set; }
    
    public virtual DbSet<OneTimePurchase> OneTimePurchases { get; set; }
    
    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }
    
    public virtual DbSet<SubscriptionLimit> SubscriptionLimits { get; set; }
}