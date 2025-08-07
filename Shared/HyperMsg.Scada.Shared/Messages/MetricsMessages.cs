namespace HyperMsg.Scada.Shared.Messages;

using HyperMsg.Scada.Shared.Models;

public record struct DeviceMetricsRequest(
    string UserId,
    string? DeviceId = null,
    DateTime? StartTime = null,
    DateTime? EndTime = null
);

public record struct DeviceMetricsResponse(
    IEnumerable<Metric> Metrics,
    string? DeviceId = null,
    DateTime? StartTime = null,
    DateTime? EndTime = null
);