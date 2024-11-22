using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }  
    
    public Guid UserId { get; set; }
    
    public User User { get; set; } = null!;

    public required string OrderId { get; set; }
    
    public decimal Amount { get; set; } 
    
    public required string Name { get; set; }
    
    public PurchaseType PurchaseType { get; set; }
    
    public PaymentStatus PaymentStatus { get; set; }
    
    public int PaidServiceId { get; set; }

    public PaidService PaidService { get; set; } = null!;
    
    public DateTimeOffset CreationDate { get; set; }
}