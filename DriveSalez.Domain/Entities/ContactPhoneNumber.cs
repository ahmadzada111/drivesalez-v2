using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class ContactPhoneNumber
{
    public int Id { get; set; }

    public required string Number { get; set; }

    public Guid UserId { get; set; }

    public required User User { get; set; }
}