namespace HyperMsg.Scada.Shared.Models;

public record DeviceMetric(string DeviceId, DateTime Timestamp, object Payload);
