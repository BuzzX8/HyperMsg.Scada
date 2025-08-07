using HyperMsg.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace HyperMsg.Scada.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MetricController : ControllerBase
{
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<MetricController> _logger;

    public MetricController(IDispatcher dispatcher, ILogger<MetricController> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetMetricsAsync([FromQuery] DateTime? from, [FromQuery] DateTime? to, CancellationToken cancellationToken)
    {        
        return Ok();
    }

    [HttpGet("{deviceId}")]
    public async Task<IActionResult> GetMetricsByDeviceIdAsync(string deviceId, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        return Ok();
    }
}
