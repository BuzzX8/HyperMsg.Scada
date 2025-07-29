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
}
