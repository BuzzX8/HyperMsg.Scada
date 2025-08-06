using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Messages;
using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.WebApi;

public static class MessageHandlers
{
    public static void RegisterHandlers(IMessagingContext messagingContext)
    {
        messagingContext.HandlerRegistry.RegisterDeviceListRequestHandler(HandleDeviceListRequest);
        messagingContext.HandlerRegistry.RegisterDeviceRequestHandler(HandleDeviceRequest);
    }

    public static IEnumerable<Device> HandleDeviceListRequest(string _)
    {
        return
        [
            new () { Id = "1", Name = "Device 1" },
            new () { Id = "2", Name = "Device 2" }
        ];
    }

    public static Device HandleDeviceRequest(string userId, string deviceId)
    {
        return new()
        {
            Id = deviceId,
            Name = $"Device {deviceId}",
            Status = "Online",
            Type = "Sensor"
        };
    }
}
