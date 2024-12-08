using DriveSalez.Domain.Aggregates.PaymentAggregate;
using DriveSalez.Domain.Common.Enums;
using DriveSalez.Domain.Exceptions;

namespace DriveSalez.Domain.Aggregates.UserAggregate;

public class CustomUser
{
    public Guid Id { get; private set; }
    
    public Guid ApplicationUserId { get; private set; }
    
    public string? FirstName { get; private set; }
    
    public string? LastName { get; private set; }
    
    public string? RefreshToken { get; private set; }
    
    public DateTimeOffset? RefreshTokenExpiration { get; private set; }
    
    public UserSubscription? UserSubscription { get; private set; }
    
    public UserStatus UserStatus { get; private set; }
    
    public BusinessDetails? BusinessDetails { get; private set; }
    
    public DateTimeOffset CreationDate { get; private set; }
    
    public DateTimeOffset? LastUpdateDate { get; private set; }

    private readonly List<Announcement> _announcements = [];
    public IReadOnlyCollection<Announcement> Announcements => _announcements.AsReadOnly();

    private readonly List<UserLimit> _userLimits = [];
    public IReadOnlyCollection<UserLimit> UserLimits => _userLimits.AsReadOnly();

    private readonly List<Payment> _payments = [];
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    private readonly List<OneTimePurchase> _oneTimePurchases = [];
    public IReadOnlyCollection<OneTimePurchase> OneTimePurchases => _oneTimePurchases.AsReadOnly();
    
    public CustomUser(Guid applicationUserId, string? firstName, string? lastName, UserStatus userStatus)
    {
        ApplicationUserId = applicationUserId;
        FirstName = firstName;
        LastName = lastName;
        UserStatus = userStatus;
        CreationDate = DateTimeOffset.UtcNow;
    }

    public void Activate()
    {
        if (UserStatus == UserStatus.Active)
            throw new DomainException("User is already active.");
        
        UserStatus = UserStatus.Active;
        LastUpdateDate = DateTimeOffset.UtcNow;
    }

    public void ActivateUserAfterPayment(Payment payment)
    {
        ValidatePaymentForActivation(payment);
        Activate();
    }

    private void ValidatePaymentForActivation(Payment payment)
    {
        if (Id != payment.UserId)
            throw new DomainException("Payment does not belong to this user.");
        
        if (payment.PurchaseType != PurchaseType.Subscription)
            throw new DomainException("Payment is not for a subscription.");
        
        if (payment.PaymentStatus != PaymentStatus.Completed)
            throw new DomainException("Payment is not completed.");
    }

    public void UpdateBusinessDetails(BusinessDetails newDetails)
    {
        BusinessDetails = newDetails;
        LastUpdateDate = DateTimeOffset.UtcNow;
    }

    public void AddAnnouncement(Announcement announcement)
    {
        _announcements.Add(announcement);
    }

    public void AddOrUpdateLimit(UserLimit limit)
    {
        var existingLimit = _userLimits.FirstOrDefault(l => l.LimitType == limit.LimitType);
        if (existingLimit != null)
        {
            _userLimits.Remove(existingLimit);
        }

        _userLimits.Add(limit);
    }

    public void AddOneTimePurchase(LimitType limitType, int limitValue, string name, decimal price)
    {
        var newPurchase = new OneTimePurchase(limitType, limitValue, name, price);
        _oneTimePurchases.Add(newPurchase);
    }
    
    public void AddPayment(Payment payment)
    {
        _payments.Add(payment);
    }

    public bool CanModerateAnnouncements()
    {
        return UserStatus == UserStatus.Active;
    }
}