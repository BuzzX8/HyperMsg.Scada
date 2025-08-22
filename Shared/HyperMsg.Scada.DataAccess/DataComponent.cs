using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Messages;
using HyperMsg.Scada.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HyperMsg.Scada.DataAccess;

public record DataComponent(IDbContextFactory<DeviceContext> DeviceContextFactory, IDbContextFactory<DeviceTypeContext> DeviceTypeFactory) : IMessagingComponent
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
        yield return handlerRegistry.RegisterCreateDeviceRequestHandler((userId, device, ctx) => CreateDeviceAsync(userId, device, ctx));
    }

    public void Detach(IMessagingContext messagingContext)
    {
        foreach (var disposable in disposables)
        {
            disposable.Dispose();
        }
    }

    #region Device Handlers

    private async Task<IEnumerable<Device>> GetDevicesByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        using var context = DeviceContextFactory.CreateDbContext();

        var devices = await context.Devices.ToListAsync(cancellationToken);

        return devices;
    }

    private Task<Device?> GetDeviceByIdAsync(string userId, string deviceId, CancellationToken cancellationToken)
    {
        using var context = DeviceContextFactory.CreateDbContext();
        
        return context.Devices.FindAsync([deviceId], cancellationToken).AsTask();
    }

    private async Task<string> CreateDeviceAsync(string userId, Device device, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(device);

        using var context = DeviceContextFactory.CreateDbContext();

        if (string.IsNullOrWhiteSpace(device.Id))
        {
            device.Id = Guid.NewGuid().ToString();
        }

        await context.Devices.AddAsync(device, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return device.Id;
    }

    #endregion

    #region DeviceType Handlers

    #endregion
}
