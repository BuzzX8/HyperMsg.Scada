using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Messages;
using HyperMsg.Scada.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HyperMsg.Scada.DataAccess;

public record DataComponent(IDbContextFactory<DeviceContext> DeviceContextFactory, IDbContextFactory<DeviceTypeContext> DeviceTypeContext) : IMessagingComponent
{
    private readonly List<IDisposable> disposables = [];

    public void Attach(IMessagingContext messagingContext)
    {
        disposables.AddRange(RegisterHandlers(messagingContext.HandlerRegistry));
    }

    private IEnumerable<IDisposable> RegisterHandlers(IHandlerRegistry handlerRegistry)
    {
        yield return handlerRegistry.RegisterDeviceListRequestHandler(GetDevicesByUserIdAsync);
        yield return handlerRegistry.RegisterDeviceRequestHandler(GetDeviceByIdAsync!);
        yield return handlerRegistry.RegisterCreateDeviceRequestHandler(CreateDeviceAsync);
        yield return handlerRegistry.RegisterDeviceTypeListRequestHandler(GetDeviceTypesAsync);
        yield return handlerRegistry.RegisterDeviceTypeRequestHandler(GetDeviceTypeByIdAsync!);
        yield return handlerRegistry.RegisterCreateDeviceTypeRequestHandler(CreateDeviceTypeAsync);
        yield return handlerRegistry.RegisterUpdateDeviceTypeRequestHandler(UpdateDeviceTypeAsync);
        yield return handlerRegistry.RegisterDeleteDeviceTypeRequestHandler(DeleteDeviceTypeAsync);
    }

    public void Detach(IMessagingContext _) => Dispose();

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

    // Implement DeviceType handlers similarly if needed
    private async Task<IEnumerable<DeviceType>> GetDeviceTypesAsync(string _, CancellationToken cancellationToken)
    {
        using var context = DeviceTypeContext.CreateDbContext();
        var deviceTypes = await context.DeviceTypes.ToListAsync(cancellationToken);
        return deviceTypes;
    }

    private Task<DeviceType?> GetDeviceTypeByIdAsync(string _, string deviceTypeId, CancellationToken cancellationToken)
    {
        using var context = DeviceTypeContext.CreateDbContext();
        return context.DeviceTypes.FindAsync([deviceTypeId], cancellationToken).AsTask();
    }

    private async Task<string> CreateDeviceTypeAsync(string _, DeviceType deviceType, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(deviceType);
        using var context = DeviceTypeContext.CreateDbContext();
        if (string.IsNullOrWhiteSpace(deviceType.Id))
        {
            deviceType.Id = Guid.NewGuid().ToString();
        }
        await context.DeviceTypes.AddAsync(deviceType, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return deviceType.Id;
    }

    private async Task UpdateDeviceTypeAsync(string _, DeviceType deviceType, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(deviceType);
        using var context = DeviceTypeContext.CreateDbContext();
        context.DeviceTypes.Update(deviceType);
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task DeleteDeviceTypeAsync(string _, string deviceTypeId, CancellationToken cancellationToken)
    {
        using var context = DeviceTypeContext.CreateDbContext();
        var deviceType = await context.DeviceTypes.FindAsync([deviceTypeId], cancellationToken);
        if (deviceType != null)
        {
            context.DeviceTypes.Remove(deviceType);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    #endregion

    public void Dispose()
    {
        foreach (var disposable in disposables)
        {
            disposable.Dispose();
        }
    }
}
