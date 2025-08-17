using HyperMsg.Scada.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HyperMsg.Scada.DataAccess;

public class DeviceTypeContext(DbContextOptions<DeviceTypeContext> options) : DbContext(options)
{
    public DbSet<DeviceType> DeviceTypes { get; set; }

    public ValueTask<DeviceType?> GetDeviceTypeByIdAsync(string id)
    {
        return DeviceTypes.FindAsync(id);
    }

    public IAsyncEnumerable<DeviceType> GetDeviceTypesAsync()
    {
        return DeviceTypes.AsAsyncEnumerable<DeviceType>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DeviceTypeConfiguration());
    }
}
