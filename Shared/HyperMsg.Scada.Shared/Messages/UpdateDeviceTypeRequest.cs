using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.Shared.Messages;

public record struct UpdateDeviceTypeRequest(string UserId, DeviceType DeviceType);
