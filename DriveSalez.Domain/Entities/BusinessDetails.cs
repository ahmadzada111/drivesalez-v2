using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class BusinessDetails
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
 
    public User User { get; set; } = null!;

    public ICollection<WorkHour> WorkHours { get; set; } = [];
    
    public string? BusinessName { get; set; }

    public ICollection<ContactPhoneNumber> PhoneNumbers { get; set; } = [];
    
    public ProfileImage? ProfilePhotoUrl { get; set; } 

    public ProfileImage? BackgroundPhotoUrl { get; set; }
    
    public string? Address { get; set; }
    
    public string? Description { get; set; }
}