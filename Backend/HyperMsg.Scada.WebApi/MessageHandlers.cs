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

    public static DeviceListResponse HandleDeviceListRequest(DeviceListRequest request)
    {
        // Logic to handle device list request
        // This is a placeholder implementation
        var devices = new List<Device>
        {
            new () { Id = "1", Name = "Device 1" },
            new () { Id = "2", Name = "Device 2" }
        };

        return new DeviceListResponse(devices);
    }

    public static DeviceResponse HandleDeviceRequest(DeviceRequest request)
    {
        // Logic to handle device request
        // This is a placeholder implementation
        return new DeviceResponse(new Device
        {
            Id = request.DeviceId,
            Name = $"Device {request.DeviceId}",
            Status = "Online",
            Type = "Sensor"
        });
    }
}
