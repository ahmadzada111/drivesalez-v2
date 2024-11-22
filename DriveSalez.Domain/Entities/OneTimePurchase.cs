using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class OneTimePurchase : PaidService
{
    public LimitType LimitType { get; set; }

    public int LimitValue { get; set; }
    
    public ICollection<User> Users { get; set; } = [];
}