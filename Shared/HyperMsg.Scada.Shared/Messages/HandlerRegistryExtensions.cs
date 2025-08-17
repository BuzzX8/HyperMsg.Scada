using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.Shared.Messages;

public static class HandlerRegistryExtensions
{
    #region Device Requests

    public static IDisposable RegisterDeviceListRequestHandler(
        this IHandlerRegistry handlersRegistry,
        RequestHandler<string, IEnumerable<Device>> handler)
    {
        RequestHandler<DeviceListRequest, DeviceListResponse> requestHandler = request =>
        {
            var response = handler(request.UserId);
            return new(response);
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterDeviceListRequestHandler(
        this IHandlerRegistry handlersRegistry,
        AsyncRequestHandler<string, IEnumerable<Device>> handler)
    {
        AsyncRequestHandler<DeviceListRequest, DeviceListResponse> requestHandler = async (request, ctx) =>
        {
            var response = await handler(request.UserId, ctx);
            return new(response);
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterDeviceRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, string, Device> handler)
    {
        RequestHandler<DeviceRequest, DeviceResponse> requestHandler = request =>
        {
            var device = handler(request.UserId, request.DeviceId);
            return new(device);
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterDeviceRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, string, CancellationToken, Task<Device>> handler)
    {
        AsyncRequestHandler<DeviceRequest, DeviceResponse> requestHandler = async (request, ctx) =>
        {
            var device = await handler(request.UserId, request.DeviceId, ctx);
            return new(device);
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterCreateDeviceRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, Device, string> handler)
    {
        RequestHandler<CreateDeviceRequest, CreateDeviceResponse> requestHandler = request =>
        {
            var deviceId = handler(request.UserId, request.Device);
            return new(deviceId);
        };
        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterCreateDeviceRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, Device, CancellationToken, Task<string>> handler)
    {
        AsyncRequestHandler<CreateDeviceRequest, CreateDeviceResponse> requestHandler = async (request, ctx) =>
        {
            var deviceId = await handler(request.UserId, request.Device, ctx);
            return new(deviceId);
        };
        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterUpdateDeviceRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Action<string, Device> handler)
    {
        RequestHandler<UpdateDeviceRequest, UpdateDeviceResponse> requestHandler = request =>
        {
            handler(request.UserId, request.Device);
            return new();
        };
        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterUpdateDeviceRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, Device, Task> handler)
    {
        AsyncRequestHandler<UpdateDeviceRequest, UpdateDeviceResponse> requestHandler = async (request, ctx) =>
        {
            await handler(request.UserId, request.Device);
            return new();
        };
        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    #endregion

    #region Device Type Requests

    public static IDisposable RegisterDeviceTypeListRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, IEnumerable<DeviceType>> handler)
    {
        RequestHandler<DeviceTypeListRequest, DeviceTypeListResponse> requestHandler = request =>
        {
            var response = handler(request.UserId);

            return new(response);
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterDeviceTypeListRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, CancellationToken, Task<IEnumerable<DeviceType>>> handler)
    {
        AsyncRequestHandler<DeviceTypeListRequest, DeviceTypeListResponse> requestHandler = async (request, ctx) =>
        {
            var response = await handler(request.UserId, ctx);
            return new(response);
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterDeviceTypeRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, string, DeviceType> handler)
    {
        RequestHandler<DeviceTypeRequest, DeviceTypeResponse> requestHandler = request =>
        {
            var deviceType = handler(request.UserId, request.DeviceTypeId);

            return new(deviceType);
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterDeviceTypeRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, string, CancellationToken, Task<DeviceType>> handler)
    {
        AsyncRequestHandler<DeviceTypeRequest, DeviceTypeResponse> requestHandler = async (request, ctx) =>
        {
            var deviceType = await handler(request.UserId, request.DeviceTypeId, ctx);
            return new(deviceType);
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterCreateDeviceTypeRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<DeviceType, string> handler)
    {
        RequestHandler<CreateDeviceTypeRequest, CreateDeviceTypeResponse> requestHandler = request =>
        {
            var deviceTypeId = handler(request.DeviceType);

            return new(deviceTypeId);
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterCreateDeviceTypeRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, DeviceType, CancellationToken, Task<string>> handler)
    {
        AsyncRequestHandler<CreateDeviceTypeRequest, CreateDeviceTypeResponse> requestHandler = async (request, ctx) =>
        {
            var deviceTypeId = await handler(request.UserId, request.DeviceType, ctx);

            return new(deviceTypeId);
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterUpdateDeviceTypeRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Action<string, DeviceType> handler)
    {
        RequestHandler<UpdateDeviceTypeRequest, UpdateDeviceTypeResponse> requestHandler = request =>
        {
            handler(request.UserId, request.DeviceType);
            return new();
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterUpdateDeviceTypeRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, DeviceType, Task> handler)
    {
        AsyncRequestHandler<UpdateDeviceTypeRequest, UpdateDeviceTypeResponse> requestHandler = async (request, ctx) =>
        {
            await handler(request.UserId, request.DeviceType);
            return new();
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    #endregion

    #region Device Metrics Requests

    public static IDisposable RegisterDeviceMetricsRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, string, DateTime?, DateTime?, IEnumerable<Metric>> handler)
    {
        RequestHandler<DeviceMetricsRequest, DeviceMetricsResponse> requestHandler = request =>
        {
            var metrics = handler(request.UserId, request.DeviceId, request.StartTime, request.EndTime);
            return new(metrics);
        };
        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterDeviceMetricsRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, string, DateTime?, DateTime?, CancellationToken, Task<IEnumerable<Metric>>> handler)
    {
        AsyncRequestHandler<DeviceMetricsRequest, DeviceMetricsResponse> requestHandler = async (request, ctx) =>
        {
            var metrics = await handler(request.UserId, request.DeviceId, request.StartTime, request.EndTime, ctx);
            return new(metrics);
        };
        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterCreateMetricRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, Metric, string> handler)
    {
        RequestHandler<CreateMetricRequest, CreateMetricResponse> requestHandler = request =>
        {
            var metric = handler(request.UserId, request.Metric);
            return new(metric);
        };
        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterCreateMetricRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<string, Metric, CancellationToken, Task<string>> handler)
    {
        AsyncRequestHandler<CreateMetricRequest, CreateMetricResponse> requestHandler = async (request, ctx) =>
        {
            var metric = await handler(request.UserId, request.Metric, ctx);
            return new(metric);
        };
        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    #endregion
}
