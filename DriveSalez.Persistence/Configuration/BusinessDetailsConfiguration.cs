using DriveSalez.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveSalez.Persistence.Configuration;

public class BusinessDetailsConfiguration : IEntityTypeConfiguration<BusinessDetails>
{
    public void Configure(EntityTypeBuilder<BusinessDetails> builder)
    {
        builder.HasKey(u => u.Id);

        builder.OwnsMany(u => u.WorkHours, wh =>
        {
            wh.ToTable("WorkHours");
            wh.WithOwner().HasForeignKey("UserId");
            wh.HasKey("Id");

            wh.Property(w => w.DayOfWeek)
                .IsRequired();

            wh.Property(w => w.OpenTime)
                .IsRequired(false);

            wh.Property(w => w.CloseTime)
                .IsRequired(false);

            wh.Property(w => w.IsClosed)
                .IsRequired();
        });
    }
}