using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.Shared.Messages;

public static class HandlerRegistryExtensions
{
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
        Func<string, CancellationToken, IEnumerable<DeviceType>> handler)
    {
        AsyncRequestHandler<DeviceTypeListRequest, DeviceTypeListResponse> requestHandler = async (request, ctx) =>
        {
            var response = handler(request.UserId, ctx);
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
            var deviceType = new DeviceType
            {
                Name = request.Name,
                //Description = request.Description,
                //MetricTemplates = request.MetricTemplates.Select(m => m.ToModel())
            };
            var deviceTypeId = handler(deviceType);

            return new(deviceTypeId);
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }

    public static IDisposable RegisterCreateDeviceTypeRequestHandler(
        this IHandlerRegistry handlersRegistry,
        Func<DeviceType, CancellationToken, Task<string>> handler)
    {
        AsyncRequestHandler<CreateDeviceTypeRequest, CreateDeviceTypeResponse> requestHandler = async (request, ctx) =>
        {
            var deviceType = new DeviceType
            {
                Name = request.Name,
                //Description = request.Description,
                //MetricTemplates = request.MetricTemplates.Select(m => m.ToModel())
            };
            var deviceTypeId = await handler(deviceType, ctx);
            return new(deviceTypeId);
        };
        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }
}
