using DriveSalez.Domain.Enums;

namespace DriveSalez.Domain.Entities;

public class OneTimePurchase : PaidService
{
    public LimitType LimitType { get; set; }

    public int LimitValue { get; set; }
}