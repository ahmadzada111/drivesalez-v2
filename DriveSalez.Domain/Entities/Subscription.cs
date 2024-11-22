using DriveSalez.Domain.Enums;
using DriveSalez.Domain.IdentityEntities;

namespace DriveSalez.Domain.Entities;

public class Subscription : PaidService
{
    public int PremiumUploadLimit { get; set; }
    
    public int RegularUploadLimit { get; set; }

    public DateTimeOffset ExpirationDate { get; set; }
    
    public UserType UserType { get; set; }
    
    public ICollection<User> Users { get; set; } = [];
}