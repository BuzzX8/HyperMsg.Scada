using HyperMsg.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HyperMsg.Scada.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeviceRepository(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDbContext<DeviceContext>(optionsAction);
        services.AddDbContextFactory<DeviceContext>(optionsAction);
        services.AddScoped<IDeviceRepository, DeviceContext>();
        
        return services;
    }

    public static IServiceCollection AddDeviceTypeRepository(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDbContextFactory<DeviceTypeContext>(optionsAction);
        services.AddScoped<IDeviceTypeRepository, DeviceTypeContext>();
        
        return services;
    }

    public static IServiceCollection AddDataAccessRepositories(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDeviceRepository(optionsAction);
        services.AddDeviceTypeRepository(optionsAction);

        return services;
    }

    public static IServiceCollection AddDataComponent(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        
        services.AddMessagingComponent<DataComponent>();

        return services;
    }
}
