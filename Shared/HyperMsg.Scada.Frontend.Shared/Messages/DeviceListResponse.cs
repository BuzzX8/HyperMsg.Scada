using HyperMsg.Scada.Frontend.Shared.Models;

namespace HyperMsg.Scada.Frontend.Shared.Messages;

public record DeviceListResponse(IEnumerable<Device> Devices);