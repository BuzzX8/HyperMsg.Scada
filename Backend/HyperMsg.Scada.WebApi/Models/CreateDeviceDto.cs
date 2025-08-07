using System.ComponentModel;

namespace HyperMsg.Scada.WebApi.Models;

[DisplayName("CreateDevice")]
[Description("Create a new device")]
public class CreateDeviceDto
{
    public required string Name { get; set; }

    public required string Type { get; set; }

    public string Description { get; set; } = string.Empty;
}
