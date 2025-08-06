using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.Shared.Messages;

public record struct DeviceTypeListResponse(IEnumerable<DeviceType> DeviceTypes);
