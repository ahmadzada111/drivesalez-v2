using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class UserLimit
{
    public int Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; } = null!;

    public LimitType LimitType { get; set; }
    
    public int LimitValue { get; set; }
    
    public int UsedValue { get; set; }
}