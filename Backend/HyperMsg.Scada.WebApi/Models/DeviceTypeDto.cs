using System.Runtime.InteropServices.JavaScript;

namespace HyperMsg.Scada.WebApi.Models;

public class DeviceTypeDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the device type.
    /// </summary>
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the name of the device type.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the description of the device type.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    public IEnumerable<JSObject> MetricTemplates { get; set; } = [];

    public IEnumerable<JSObject> CommandTemplates { get; set; } = [];
}
