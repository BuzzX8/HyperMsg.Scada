using System.Runtime.InteropServices.JavaScript;

namespace HyperMsg.Scada.WebApi.Models
{
    public class CreateDeviceTypeDto
    {
        /// <summary>
        /// Gets or sets the name associated with the object.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public IEnumerable<JSObject> MetricTemplates { get; set; } = [];

        public IEnumerable<JSObject> CommandTemplates { get; set; } = [];
    }
}
