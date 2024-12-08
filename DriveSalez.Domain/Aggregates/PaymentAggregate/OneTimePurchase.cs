using DriveSalez.Domain.Aggregates.UserAggregate;
using DriveSalez.Domain.Common.Enums;

namespace DriveSalez.Domain.Aggregates.PaymentAggregate;

public class OneTimePurchase : PaidService
{
    public LimitType LimitType { get; set; }

    public int LimitValue { get; set; }
    
    public ICollection<CustomUser> Users { get; set; } = [];
    
    public OneTimePurchase(LimitType limitType, int limitValue, string name, decimal price) : base(name, price)
    {
        LimitType = limitType;
        LimitValue = limitValue;
    }
}