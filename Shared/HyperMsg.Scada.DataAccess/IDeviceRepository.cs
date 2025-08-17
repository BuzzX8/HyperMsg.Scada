using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.DataAccess;

public interface IDeviceRepository
{
    ValueTask<Device?> GetDeviceByIdAsync(string id);
    IAsyncEnumerable<Device> GetDevicesAsync();
    ValueTask<string> CreateDevice(Device device);
}
