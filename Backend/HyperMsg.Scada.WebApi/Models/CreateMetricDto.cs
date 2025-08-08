using System.Text.Json.Nodes;

namespace HyperMsg.Scada.WebApi.Models;

public class CreateMetricDto
{
    public string MetricName { get; set; } = string.Empty;

    public string DeviceId { get; set; } = string.Empty;

    public JsonObject Value { get; set; } = [];
}
