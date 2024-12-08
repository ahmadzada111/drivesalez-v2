using DriveSalez.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class ContactPhoneNumberConfiguration : IEntityTypeConfiguration<ContactPhoneNumber>
{
    public void Configure(EntityTypeBuilder<ContactPhoneNumber> builder)
    { 
        builder.ToTable("ContactPhoneNumbers");

        builder.HasKey("Id");
        
        builder.HasOne<CustomUser>()
            .WithMany()
            .HasForeignKey("UserId");
        
        builder.Property(x => x.Number)
            .IsRequired();
    }
}