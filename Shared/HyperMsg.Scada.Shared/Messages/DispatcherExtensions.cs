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
}
