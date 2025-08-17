using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.DataAccess
{
    public interface IDeviceTypeRepository
    {
        ValueTask<DeviceType?> GetDeviceTypeByIdAsync(string id);
        IAsyncEnumerable<DeviceType> GetDeviceTypesAsync();
        ValueTask<string> CreateDeviceType(DeviceType deviceType);
        ValueTask UpdateDeviceType(DeviceType deviceType);
        ValueTask DeleteDeviceType(string id);
    }
}