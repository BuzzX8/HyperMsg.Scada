using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.Shared.Messages;

public record struct CreateDeviceTypeRequest(string UserId, DeviceType DeviceType);

public record struct CreateDeviceTypeResponse(string DeviceTypeId);

public record struct DeviceTypeListRequest(string UserId);

public record struct DeviceTypeListResponse(IEnumerable<DeviceType> DeviceTypes);

public record struct DeviceTypeRequest(string UserId, string DeviceTypeId);

public record struct DeviceTypeResponse(DeviceType DeviceType);

public record struct UpdateDeviceTypeRequest(string UserId, DeviceType DeviceType);

public record struct UpdateDeviceTypeResponse();

public record struct DeleteDeviceTypeRequest(string UserId, string DeviceTypeId);

public record struct DeleteDeviceTypeResponse();