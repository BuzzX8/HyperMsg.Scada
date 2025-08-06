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
            return new DeviceListResponse(response);
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
            return new DeviceListResponse(response);
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
            return new DeviceResponse(device);
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
            return new DeviceResponse(device);
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

            return new DeviceTypeResponse(deviceType);
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
            return new DeviceTypeResponse(deviceType);
        };

        return handlersRegistry.RegisterRequestHandler(requestHandler);
    }
}
