using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Messages;
using HyperMsg.Scada.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HyperMsg.Scada.DataAccess;

public record DataComponent(IDbContextFactory<DeviceContext> ContextFactory) : IMessagingComponent
{
    private readonly List<IDisposable> disposables = [];

    public void Attach(IMessagingContext messagingContext)
    {
        disposables.AddRange(RegisterHandlers(messagingContext.HandlerRegistry));
    }

    private IEnumerable<IDisposable> RegisterHandlers(IHandlerRegistry handlerRegistry)
    {
        yield return handlerRegistry.RegisterDeviceListRequestHandler((userId, ctx) => GetDevicesByUserIdAsync(userId, ctx));
        yield return handlerRegistry.RegisterDeviceRequestHandler((userId, deviceId, ctx) => GetDeviceByIdAsync(userId, deviceId, ctx)!);
    }

    public void Detach(IMessagingContext messagingContext)
    {
        foreach (var disposable in disposables)
        {
            disposable.Dispose();
        }
    }

    private async Task<IEnumerable<Device>> GetDevicesByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        using var context = ContextFactory.CreateDbContext();

        var devices = await context.Devices.ToListAsync(cancellationToken);

        return devices;
    }

    private Task<Device?> GetDeviceByIdAsync(string userId, string deviceId, CancellationToken cancellationToken)
    {
        using var context = ContextFactory.CreateDbContext();
        
        return context.Devices.FindAsync([deviceId], cancellationToken).AsTask();
    }
}
