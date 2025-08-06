using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.Shared.Messages;

public static class DispatcherExtensions
{
    public static IEnumerable<Device> DispatchDeviceListRequest(this IDispatcher dispatcher, string userId)
    {
        var response = dispatcher.DispatchRequest<DeviceListRequest, DeviceListResponse>(new(userId));

        return response.Devices;
    }

    public static async Task<IEnumerable<Device>> DispatchDeviceListRequestAsync(this IDispatcher dispatcher, string userId, CancellationToken cancellationToken)
    {
        var response = await dispatcher.DispatchRequestAsync<DeviceListRequest, DeviceListResponse>(new(userId), cancellationToken);

        return response.Devices;
    }

    public static Device DispatchDeviceRequest(this IDispatcher dispatcher, string deviceId)
    {
        var response = dispatcher.DispatchRequest<DeviceRequest, DeviceResponse>(new() { DeviceId = deviceId });

        return response.Device;
    }

    public static async Task<Device> DispatchDeviceRequestAsync(this IDispatcher dispatcher, string deviceId, CancellationToken cancellationToken)
    {
        var response = await dispatcher.DispatchRequestAsync<DeviceRequest, DeviceResponse>(new() { DeviceId = deviceId }, cancellationToken);

        return response.Device;
    }

    public static IEnumerable<DeviceType> DispatchDeviceTypeListRequest(this IDispatcher dispatcher)
    {
        var response = dispatcher.DispatchRequest<DeviceTypeListRequest, DeviceTypeListResponse>(new());
        
        return response.DeviceTypes;
    }

    public static async Task<IEnumerable<DeviceType>> DispatchDeviceTypeListRequestAsync(this IDispatcher dispatcher, CancellationToken cancellationToken)
    {
        var response = await dispatcher.DispatchRequestAsync<DeviceTypeListRequest, DeviceTypeListResponse>(new(), cancellationToken);
        
        return response.DeviceTypes;
    }

    public static DeviceType DispatchDeviceTypeRequest(this IDispatcher dispatcher, string deviceTypeId)
    {
        var response = dispatcher.DispatchRequest<DeviceTypeRequest, DeviceTypeResponse>(new() { DeviceTypeId = deviceTypeId });

        return response.DeviceType;
    }

    public static async Task<DeviceType> DispatchDeviceTypeRequestAsync(this IDispatcher dispatcher, string deviceTypeId, CancellationToken cancellationToken)
    {
        var response = await dispatcher.DispatchRequestAsync<DeviceTypeRequest, DeviceTypeResponse>(new() { DeviceTypeId = deviceTypeId }, cancellationToken);

        return response.DeviceType;
    }

    public static string DispatchCreateDeviceTypeRequest(this IDispatcher dispatcher, DeviceType request)
    {
        var createRequest = new CreateDeviceTypeRequest
        {
            Name = request.Name,
            //Description = request.Description,
            //MetricTemplates = request.MetricTemplates.Select(m => m.ToDto()).ToList()
        };
        var response = dispatcher.DispatchRequest<CreateDeviceTypeRequest, CreateDeviceTypeResponse>(createRequest);

        return response.DeviceTypeId;
    }

    public static async Task<string> DispatchCreateDeviceTypeRequestAsync(this IDispatcher dispatcher, DeviceType request, CancellationToken cancellationToken)
    {
        var createRequest = new CreateDeviceTypeRequest
        {
            Name = request.Name,
            //Description = request.Description,
            //MetricTemplates = request.MetricTemplates.Select(m => m.ToDto()).ToList()
        };
        var response = await dispatcher.DispatchRequestAsync<CreateDeviceTypeRequest, CreateDeviceTypeResponse>(createRequest, cancellationToken);

        return response.DeviceTypeId;
    }
}
