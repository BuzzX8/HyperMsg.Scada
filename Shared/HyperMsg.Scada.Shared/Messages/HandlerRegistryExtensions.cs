using HyperMsg.Messaging;

namespace HyperMsg.Scada.Shared.Messages;

public static class HandlerRegistryExtensions
{
    public static IDisposable RegisterDeviceListRequestHandler(
        this IHandlerRegistry handlersRegistry,
        RequestHandler<DeviceListRequest, DeviceListResponse> handler)
    {
        return handlersRegistry.RegisterRequestHandler(handler);
    }

    public static IDisposable RegisterDeviceListRequestHandler(
        this IHandlerRegistry handlersRegistry,
        AsyncRequestHandler<DeviceListRequest, DeviceListResponse> handler)
    {
        return handlersRegistry.RegisterRequestHandler(handler);
    }

    public static IDisposable RegisterDeviceRequestHandler(
        this IHandlerRegistry handlersRegistry,
        RequestHandler<DeviceRequest, DeviceResponse> handler)
    {
        return handlersRegistry.RegisterRequestHandler(handler);
    }

    public static IDisposable RegisterDeviceRequestHandler(
        this IHandlerRegistry handlersRegistry,
        AsyncRequestHandler<DeviceRequest, DeviceResponse> handler)
    {
        return handlersRegistry.RegisterRequestHandler(handler);
    }

    public static IDisposable RegisterDeviceTypeListRequestHandler(
        this IHandlerRegistry handlersRegistry,
        RequestHandler<DeviceTypeListRequest, DeviceTypeListResponse> handler)
    {
        return handlersRegistry.RegisterRequestHandler(handler);
    }

    public static IDisposable RegisterDeviceTypeListRequestHandler(
        this IHandlerRegistry handlersRegistry,
        AsyncRequestHandler<DeviceTypeListRequest, DeviceTypeListResponse> handler)
    {
        return handlersRegistry.RegisterRequestHandler(handler);
    }

    public static IDisposable RegisterDeviceTypeRequestHandler(
        this IHandlerRegistry handlersRegistry,
        RequestHandler<DeviceTypeRequest, DeviceTypeResponse> handler)
    {
        return handlersRegistry.RegisterRequestHandler(handler);
    }

    public static IDisposable RegisterDeviceTypeRequestHandler(
        this IHandlerRegistry handlersRegistry,
        AsyncRequestHandler<DeviceTypeRequest, DeviceTypeResponse> handler)
    {
        return handlersRegistry.RegisterRequestHandler(handler);
    }
}
