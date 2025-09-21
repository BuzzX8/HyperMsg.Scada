using System.Text.Json.Nodes;

namespace HyperMsg.Scada.WebApi.Models
{
    public class CreateDeviceTypeDto
    {
        /// <summary>
        /// Gets or sets the name associated with the object.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public IEnumerable<JsonObject> MetricTemplates { get; set; } = [];

        public IEnumerable<JsonObject> CommandTemplates { get; set; } = [];
    }
}
