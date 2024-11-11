using DriveSalez.Domain.Entities;
using DriveSalez.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Persistence.DbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public DbSet<BaseUser> BaseUsers { get; set; }
    
    public DbSet<Image> ImageUrls { get; set; }
    
    public DbSet<WorkHour> WorkHours { get; set; }
    
    public DbSet<Announcement> Announcements { get; set; }
}