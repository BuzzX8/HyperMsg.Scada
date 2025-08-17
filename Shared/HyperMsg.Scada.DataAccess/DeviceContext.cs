using HyperMsg.Scada.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HyperMsg.Scada.DataAccess;

public class DeviceContext(DbContextOptions<DeviceContext> options) : DbContext(options)
{
    public DbSet<Device> Devices { get; set; }

    public Task<Device?> GetDeviceByIdAsync(string id)
    {
        return Devices.FirstOrDefaultAsync(d => d.Id == id);
    }

    public IAsyncEnumerable<Device> GetAllDevicesAsync()
    {
        return Devices.AsAsyncEnumerable();
    }
}
