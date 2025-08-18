using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Messages;
using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.DataAccess;

public record DataComponent(IDeviceRepository DeviceRepository) : IMessagingComponent
{
    private readonly List<IDisposable> disposables = [];

    public void Attach(IMessagingContext messagingContext)
    {
        disposables.AddRange(RegisterHandlers(messagingContext.HandlerRegistry));
    }

    private IEnumerable<IDisposable> RegisterHandlers(IHandlerRegistry handlerRegistry)
    {
        yield return handlerRegistry.RegisterDeviceListRequestHandler((userId, ctx) => GetDevicesByUserIdAsync(DeviceRepository, userId, ctx));
        yield return handlerRegistry.RegisterDeviceRequestHandler((userId, deviceId, ctx) => GetDeviceByIdAsync(DeviceRepository, userId, deviceId, ctx));
    }

    public void Detach(IMessagingContext messagingContext)
    {
        foreach (var disposable in disposables)
        {
            disposable.Dispose();
        }
    }

    private static Task<IEnumerable<Device>> GetDevicesByUserIdAsync(IDeviceRepository repository, string userId, CancellationToken cancellationToken)
    {
        var devices = repository.GetDevicesAsync();

        throw new NotImplementedException("The method GetDevicesByUserIdAsync is not implemented yet. Please implement this method to retrieve devices by user ID.");
    }

    private static Task<Device?> GetDeviceByIdAsync(IDeviceRepository repository, string userId, string deviceId, CancellationToken cancellationToken)
    {
        return repository.GetDeviceByIdAsync(deviceId).AsTask();
    }
}
