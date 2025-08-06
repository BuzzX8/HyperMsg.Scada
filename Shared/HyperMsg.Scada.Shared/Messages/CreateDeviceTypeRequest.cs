using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.Shared.Messages;

public record struct CreateDeviceTypeRequest(string UserId, DeviceType DeviceType);
