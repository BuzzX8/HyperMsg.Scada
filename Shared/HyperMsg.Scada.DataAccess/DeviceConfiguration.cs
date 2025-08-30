using HyperMsg.Scada.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HyperMsg.Scada.DataAccess;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(d => d.Status)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(d => d.Type)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasIndex(d => d.Name)
            .IsUnique();
        builder.HasIndex(d => d.Type);

        //builder.Property(d => d.Metadata);
    }
}
