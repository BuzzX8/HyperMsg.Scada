namespace HyperMsg.Scada.Shared.Models;

public record Metric(string DeviceId, DateTime Timestamp, object Payload);
