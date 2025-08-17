using Microsoft.EntityFrameworkCore;
using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.DataAccess;

public class DeviceContext(DbContextOptions<DeviceContext> options) : DbContext(options), IDeviceRepository
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

    public async ValueTask<string> CreateDevice(Device device)
    {
        ArgumentNullException.ThrowIfNull(device);
        if (string.IsNullOrWhiteSpace(device.Id))
        {
            device.Id = Guid.NewGuid().ToString();
        }
        await Devices.AddAsync(device);
        await SaveChangesAsync();
        return device.Id;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DeviceConfiguration());
    }
}
