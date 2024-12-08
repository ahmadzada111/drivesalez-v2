using DriveSalez.Domain.Common.Enums;

namespace DriveSalez.Domain.Aggregates.UserAggregate;

public class UserLimit
{
    public int Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public CustomUser CustomUser { get; set; } = null!;

    public LimitType LimitType { get; set; }
    
    public int LimitValue { get; set; }
    
    public int UsedValue { get; set; }
}