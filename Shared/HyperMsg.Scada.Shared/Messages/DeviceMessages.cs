namespace HyperMsg.Scada.Shared.Messages;

public record struct CreateDeviceRequest(
    string UserId,
    Models.Device Device
);

public record struct CreateDeviceResponse(string DeviceId);

public record struct DeviceListRequest(string UserId);

public record struct DeviceListResponse(IEnumerable<Models.Device> Devices);

public record struct DeviceRequest(string UserId, string DeviceId);

public record struct DeviceResponse(Models.Device Device);