namespace DriveSalez.Domain.IdentityEntities;

public class BaseUser
{
    public Guid Id { get; set; }
    
    public Guid IdentityId { get; set; }

    public ApplicationUser ApplicationUser { get; set; } = null!;

    public string? FirstName { get; set; }
        
    public string? LastName { get; set; }
    
    public string? RefreshToken { get; set; }
        
    public DateTimeOffset? RefreshTokenExpiration { get; set; }
        
    public DateTimeOffset CreationDate { get; set; }
        
    public DateTimeOffset? LastUpdateDate { get; set; }
}