using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class WorkHour
{
    public int Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public required User User { get; set; }
    
    public DayOfWeek DayOfWeek { get; set; }
    
    public TimeSpan? OpenTime { get; set; }
    
    public TimeSpan? CloseTime { get; set; } 
    
    public bool IsClosed { get; set; }
}