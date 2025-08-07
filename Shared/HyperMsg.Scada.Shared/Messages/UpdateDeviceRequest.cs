using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.Shared.Messages;

public record struct UpdateDeviceRequest(string UserId, string DeviceId, Device Device);
