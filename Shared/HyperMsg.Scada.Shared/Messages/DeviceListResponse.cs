using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.Shared.Messages;

public record DeviceListResponse(IEnumerable<Device> Devices);