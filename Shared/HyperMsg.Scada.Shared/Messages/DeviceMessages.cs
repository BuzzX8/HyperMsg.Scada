using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.Shared.Messages;

public record struct CreateDeviceRequest(
    string UserId,
    Device Device
);

public record struct CreateDeviceResponse(string DeviceId);

public record struct DeviceListRequest(string UserId);

public record struct DeviceListResponse(IEnumerable<Device> Devices);

public record struct DeviceRequest(string UserId, string DeviceId);

public record struct DeviceResponse(Device Device);

public record struct UpdateDeviceRequest(string UserId, string DeviceId, Device Device);

public record struct UpdateDeviceResponse();