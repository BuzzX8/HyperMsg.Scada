namespace HyperMsg.Scada.Frontend.Shared;

public class Device
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime LastUpdated { get; set; }
}
