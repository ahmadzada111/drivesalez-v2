using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.Exceptions;

namespace DriveSalez.Domain.IdentityEntities;

public class User : BaseUser
{
    public ICollection<Announcement> Announcements { get; set; } = [];
    
    public ICollection<UserLimit> UserLimits { get; set; } = [];
    
    public ICollection<Payment> Payments { get; set; } = [];

    public ICollection<OneTimePurchase> OneTimePurchases { get; set; } = [];
    
    public UserSubscription? UserSubscription { get; set; }

    public UserStatus UserStatus { get; set; }
    
    public BusinessDetails? BusinessDetails { get; set; }

    private void Activate()
    {
        if (UserStatus == UserStatus.Active)
            throw new DomainException("User is already active");
        
        UserStatus = UserStatus.Active;
    }

    public void ActivateUserAfterPayment(Payment payment)
    {
        if (Id != payment.UserId)
            throw new DomainException("Payment does not belong to this user.");
        
        if (payment.PurchaseType != PurchaseType.Subscription)
            throw new DomainException("Payment is not for a subscription.");
        
        if (payment.PaymentStatus != PaymentStatus.Completed)
            throw new DomainException("Payment is not completed.");
        
        Activate();
    }
}