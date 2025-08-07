using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace HyperMsg.Scada.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MetricsController : ControllerBase
{
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<MetricsController> _logger;

    public MetricsController(IDispatcher dispatcher, ILogger<MetricsController> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetMetricsAsync([FromQuery] DateTime? from, [FromQuery] DateTime? to, CancellationToken cancellationToken)
    {
        var metrics = await _dispatcher.DispatchDeviceMetricsRequestAsync("", null, from, to, cancellationToken);

        return Ok();
    }

    [HttpGet("{deviceId}")]
    public async Task<IActionResult> GetMetricsByDeviceIdAsync(string deviceId, [FromQuery] DateTime? from, [FromQuery] DateTime? to, CancellationToken cancellationToken)
    {
        var metrics = await _dispatcher.DispatchDeviceMetricsRequestAsync("", deviceId, from, to, cancellationToken);

        return Ok();
    }
}
