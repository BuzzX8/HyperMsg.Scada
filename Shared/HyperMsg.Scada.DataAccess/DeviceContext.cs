using Microsoft.EntityFrameworkCore;
using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.DataAccess;

public class DeviceContext(DbContextOptions<DeviceContext> options) : DbContext(options)
{
    public DbSet<Device> Devices { get; set; }

    public ValueTask<Device?> GetDeviceByIdAsync(string id)
    {
        return Devices.FindAsync(id);
    }

    public IAsyncEnumerable<Device> GetDevicesAsync()
    {
        return Devices.AsAsyncEnumerable<Device>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DeviceConfiguration());
    }
}
