using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class ProfileImage : Image
{
    public Guid UserId { get; set; }
    
    public required User User { get; set; }
}