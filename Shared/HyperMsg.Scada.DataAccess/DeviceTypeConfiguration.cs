using HyperMsg.Scada.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HyperMsg.Scada.DataAccess;

public class DeviceTypeConfiguration : IEntityTypeConfiguration<DeviceType>
{
    public void Configure(EntityTypeBuilder<DeviceType> builder)
    {
        builder.HasKey(dt => dt.Id);
        builder.Property(dt => dt.Id)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(dt => dt.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(dt => dt.Description)
            .HasMaxLength(500);
        builder.HasIndex(dt => dt.Name)
            .IsUnique();

        // Additional configurations can be added here if needed
    }
}
