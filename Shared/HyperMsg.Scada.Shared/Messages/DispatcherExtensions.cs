using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.Shared.Messages;

public static class DispatcherExtensions
{
    public static IEnumerable<Device> DispatchDeviceListRequest(this IDispatcher dispatcher)
    {
        var response = dispatcher.DispatchRequest<DeviceListRequest, DeviceListResponse>(new());

        return response?.Devices ?? [];
    }

    public static async Task<IEnumerable<Device>> DispatchDeviceListRequestAsync(this IDispatcher dispatcher, CancellationToken cancellationToken)
    {
        var response = await dispatcher.DispatchRequestAsync<DeviceListRequest, DeviceListResponse>(new(), cancellationToken);

        return response?.Devices ?? [];
    }

    public static Device DispatchDeviceRequest(this IDispatcher dispatcher, string deviceId)
    {
        var response = dispatcher.DispatchRequest<DeviceRequest, DeviceResponse>(new() { DeviceId = deviceId });

        return response.Device ?? new Device();
    }

    public static async Task<Device> DispatchDeviceRequestAsync(this IDispatcher dispatcher, string deviceId, CancellationToken cancellationToken)
    {
        var response = await dispatcher.DispatchRequestAsync<DeviceRequest, DeviceResponse>(new() { DeviceId = deviceId }, cancellationToken);

        return response.Device ?? new Device();
    }

    public static IEnumerable<DeviceType> DispatchDeviceTypeListRequest(this IDispatcher dispatcher)
    {
        var response = dispatcher.DispatchRequest<DeviceTypeListRequest, DeviceTypeListResponse>(new());
        
        return response.DeviceTypes ?? [];
    }

    public static async Task<IEnumerable<DeviceType>> DispatchDeviceTypeListRequestAsync(this IDispatcher dispatcher, CancellationToken cancellationToken)
    {
        var response = await dispatcher.DispatchRequestAsync<DeviceTypeListRequest, DeviceTypeListResponse>(new(), cancellationToken);
        
        return response.DeviceTypes ?? [];
    }
}
