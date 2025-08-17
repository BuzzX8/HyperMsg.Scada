using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;

namespace HyperMsg.Scada.WebApi.Models;

public class CreateMetricDto
{
    [Description("The name of the metric to be created.")]
    public string MetricName { get; set; } = string.Empty;

    [Required]
    [Description("The ID of the device associated with the metric.")]
    public string DeviceId { get; set; } = string.Empty;

    public JsonObject Payload { get; set; } = [];
}
