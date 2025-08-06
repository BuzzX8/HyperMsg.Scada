namespace HyperMsg.Scada.Shared.Models;

public class DeviceType
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
}
