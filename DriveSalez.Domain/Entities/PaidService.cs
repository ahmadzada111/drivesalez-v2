using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class PaidService
{
    public int Id { get; set; }
    
    public required string Name { get; set; } 
    
    public decimal Price { get; set; }
    
    public ICollection<User> Users { get; set; } = [];
}