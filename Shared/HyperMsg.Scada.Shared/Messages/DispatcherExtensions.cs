using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.Shared.Messages;

public static class DispatcherExtensions
{
    #region Device Requests

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

    public static Device? DispatchDeviceRequest(this IDispatcher dispatcher, string deviceId)
    {
        var response = dispatcher.DispatchRequest<DeviceRequest, DeviceResponse>(new() { DeviceId = deviceId });

        return response.Device;
    }

    public static async Task<Device?> DispatchDeviceRequestAsync(this IDispatcher dispatcher, string deviceId, CancellationToken cancellationToken)
    {
        var response = await dispatcher.DispatchRequestAsync<DeviceRequest, DeviceResponse>(new() { DeviceId = deviceId }, cancellationToken);

        return response.Device;
    }

    public static string DispatchCreateDeviceRequest(this IDispatcher dispatcher, string userId, Device device)
    {
        var createRequest = new CreateDeviceRequest(userId, device);
        var response = dispatcher.DispatchRequest<CreateDeviceRequest, CreateDeviceResponse>(createRequest);

        return response.DeviceId;
    }

    public static async Task<string> DispatchCreateDeviceRequestAsync(this IDispatcher dispatcher, string userId, Device device, CancellationToken cancellationToken)
    {
        var createRequest = new CreateDeviceRequest(userId, device);
        var response = await dispatcher.DispatchRequestAsync<CreateDeviceRequest, CreateDeviceResponse>(createRequest, cancellationToken);

        return response.DeviceId;
    }

    public static void DispatchUpdateDeviceRequest(this IDispatcher dispatcher, string userId, string deviceId, Device device)
    {
        var updateRequest = new UpdateDeviceRequest(userId, deviceId, device);

        dispatcher.DispatchRequest<UpdateDeviceRequest, UpdateDeviceResponse>(updateRequest);
    }

    public static async Task DispatchUpdateDeviceRequestAsync(this IDispatcher dispatcher, string userId, string deviceId, Device device, CancellationToken cancellationToken)
    {
        var updateRequest = new UpdateDeviceRequest(userId, deviceId, device);

        await dispatcher.DispatchRequestAsync<UpdateDeviceRequest, UpdateDeviceResponse>(updateRequest, cancellationToken);
    }

    #endregion

    #region Device Type Requests

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

    public static DeviceType? DispatchDeviceTypeRequest(this IDispatcher dispatcher, string deviceTypeId)
    {
        var response = dispatcher.DispatchRequest<DeviceTypeRequest, DeviceTypeResponse>(new() { DeviceTypeId = deviceTypeId });

        return response.DeviceType;
    }

    public static async Task<DeviceType?> DispatchDeviceTypeRequestAsync(this IDispatcher dispatcher, string deviceTypeId, CancellationToken cancellationToken)
    {
        var response = await dispatcher.DispatchRequestAsync<DeviceTypeRequest, DeviceTypeResponse>(new() { DeviceTypeId = deviceTypeId }, cancellationToken);

        return response.DeviceType;
    }

    public static string DispatchCreateDeviceTypeRequest(this IDispatcher dispatcher, string userId, DeviceType deviceType)
    {
        var createRequest = new CreateDeviceTypeRequest(userId, deviceType);
        var response = dispatcher.DispatchRequest<CreateDeviceTypeRequest, CreateDeviceTypeResponse>(createRequest);

        return response.DeviceTypeId;
    }

    public static async Task<string> DispatchCreateDeviceTypeRequestAsync(this IDispatcher dispatcher, string userId, DeviceType deviceType, CancellationToken cancellationToken)
    {
        var createRequest = new CreateDeviceTypeRequest(userId, deviceType);
        var response = await dispatcher.DispatchRequestAsync<CreateDeviceTypeRequest, CreateDeviceTypeResponse>(createRequest, cancellationToken);

        return response.DeviceTypeId;
    }

    public static void DispatchUpdateDeviceTypeRequest(this IDispatcher dispatcher, string userId, DeviceType deviceType)
    {
        var updateRequest = new UpdateDeviceTypeRequest(userId, deviceType);

        dispatcher.DispatchRequest<UpdateDeviceTypeRequest, UpdateDeviceTypeResponse>(updateRequest);
    }

    public static async Task DispatchUpdateDeviceTypeRequestAsync(this IDispatcher dispatcher, string userId, DeviceType deviceType, CancellationToken cancellationToken)
    {
        var updateRequest = new UpdateDeviceTypeRequest(userId, deviceType);

        await dispatcher.DispatchRequestAsync<UpdateDeviceTypeRequest, UpdateDeviceTypeResponse>(updateRequest, cancellationToken);
    }

    #endregion

    #region Metrics Requests

    public static IEnumerable<Metric> DispatchDeviceMetricsRequest(this IDispatcher dispatcher, string userId, string? deviceId = null, DateTime? startTime = null, DateTime? endTime = null)
    {
        var request = new DeviceMetricsRequest(userId, deviceId, startTime, endTime);
        var response = dispatcher.DispatchRequest<DeviceMetricsRequest, DeviceMetricsResponse>(request);

        return response.Metrics;
    }

    public static async Task<IEnumerable<Metric>> DispatchDeviceMetricsRequestAsync(this IDispatcher dispatcher, string userId, string? deviceId = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken cancellationToken = default)
    {
        var request = new DeviceMetricsRequest(userId, deviceId, startTime, endTime);
        var response = await dispatcher.DispatchRequestAsync<DeviceMetricsRequest, DeviceMetricsResponse>(request, cancellationToken);

        return response.Metrics;
    }

    public static string DispatchCreateMetricRequest(this IDispatcher dispatcher, string userId, Metric metric)
    {
        var request = new CreateMetricRequest(userId, metric);
        var response = dispatcher.DispatchRequest<CreateMetricRequest, CreateMetricResponse>(request);

        return response.MetricId;
    }

    public static async Task<string> DispatchCreateMetricRequestAsync(this IDispatcher dispatcher, string userId, Metric metric, CancellationToken cancellationToken = default)
    {
        var request = new CreateMetricRequest(userId, metric);
        var response = await dispatcher.DispatchRequestAsync<CreateMetricRequest, CreateMetricResponse>(request, cancellationToken);

        return response.MetricId;
    }

    #endregion
}