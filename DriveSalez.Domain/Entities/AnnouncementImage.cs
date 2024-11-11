namespace DriveSalez.Domain.Entities;

public class AnnouncementImage : Image
{
    public Guid AnnouncementId { get; set; }
    
    public required Announcement Announcement { get; set; }
}