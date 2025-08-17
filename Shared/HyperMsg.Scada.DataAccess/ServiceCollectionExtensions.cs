using Microsoft.Extensions.DependencyInjection;

namespace HyperMsg.Scada.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeviceRepository(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddScoped<IDeviceRepository, DeviceContext>();
        return services;
    }

    public static IServiceCollection AddDeviceTypeRepository(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddScoped<IDeviceTypeRepository, DeviceTypeContext>();
        return services;
    }

    public static IServiceCollection AddDataAccessRepositories(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddDeviceRepository();
        services.AddDeviceTypeRepository();
        return services;
    }
}
