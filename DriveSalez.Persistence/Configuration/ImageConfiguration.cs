using DriveSalez.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Images");

        builder.HasKey("Id");

        builder.HasMany<BusinessDetails>()
            .WithOne(x => x.ProfileImage);

        builder.HasMany<BusinessDetails>()
            .WithOne(x => x.BackgroundImage);
        
        builder.Property(x => x.Url)
            .IsRequired();
    }
}