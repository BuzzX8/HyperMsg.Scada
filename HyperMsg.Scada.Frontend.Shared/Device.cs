using System.Collections.Concurrent;

namespace HyperMsg.Scada.Frontend.Shared;

public class Device
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public IDictionary<string, object> Metadata { get; private set; } = new ConcurrentDictionary<string, object>();
}
