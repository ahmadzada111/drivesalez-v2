using DriveSalez.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Domain.IdentityEntities;

public class ApplicationUser : IdentityUser<Guid>
{
    public required BaseUser BaseUser { get; set; }
}