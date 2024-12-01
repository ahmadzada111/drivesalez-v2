using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Domain.IdentityEntities;

public class ApplicationUser : IdentityUser<Guid>
{
    public BaseUser BaseUser { get; set; } = null!;
}