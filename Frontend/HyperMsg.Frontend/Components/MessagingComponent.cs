using HyperMsg.Messaging;
using HyperMsg.Scada.Frontend.Shared.Messages;
using HyperMsg.Scada.Frontend.Shared.Models;

namespace HyperMsg.Frontend.Components;

public class MessagingComponent : IMessagingComponent
{
    private readonly List<IDisposable> registrations = [];

    public void Attach(IMessagingContext messagingContext)
    {
        messagingContext.HandlerRegistry.RegisterRequestHandler<DeviceListRequest, DeviceListResponse>(HandleDeviceListRequestAsync);
        //registrations.Add(regisatration);
    }

    public void Detach(IMessagingContext messagingContext)
    {
        
    }

    private Task<DeviceListResponse> HandleDeviceListRequestAsync(DeviceListRequest request, CancellationToken cancellationToken)
    {
        // Handle the device list request here
        // For example, you might fetch the list of devices and send a response back
        return Task.FromResult(new DeviceListResponse(new List<Device>
            {
                new() { Id = "1", Name = "Device 1" },
                new() { Id = "2", Name = "Device 2" }
            }));        
    }
}
