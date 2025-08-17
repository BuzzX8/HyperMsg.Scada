using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Messages;
using HyperMsg.Scada.Shared.Models;
using HyperMsg.Scada.WebApi.Models;
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
    [EndpointDescription("Get all metrics for all devices")]
    [ProducesResponseType(typeof(IEnumerable<Metric>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMetricsAsync([FromQuery] DateTime? from, [FromQuery] DateTime? to, CancellationToken cancellationToken)
    {
        var metrics = await _dispatcher.DispatchDeviceMetricsRequestAsync("", null, from, to, cancellationToken);

        if (metrics is null || !metrics.Any())
        {
            _logger.LogWarning("No metrics found for the specified time range.");
            return NotFound("No metrics found for the specified time range.");
        }

        return Ok(metrics);
    }

    [HttpGet("{deviceId}")]
    [EndpointDescription("Get metrics for a specific device by its ID")]
    [ProducesResponseType(typeof(IEnumerable<Metric>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMetricsByDeviceIdAsync(string deviceId, [FromQuery] DateTime? from, [FromQuery] DateTime? to, CancellationToken cancellationToken)
    {
        var metrics = await _dispatcher.DispatchDeviceMetricsRequestAsync("", deviceId, from, to, cancellationToken);

        if (metrics is null || !metrics.Any())
        {
            _logger.LogWarning("No metrics found for device {DeviceId}", deviceId);
            return NotFound($"No metrics found for device {deviceId}");
        }

        return Ok(metrics);
    }

    [HttpPost]
    [EndpointDescription("Create a new metric for a device")]
    [ProducesResponseType(typeof(Metric), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMetricAsync([FromBody] CreateMetricDto metric, CancellationToken cancellationToken)
    {
        var userId = User?.FindFirst("sub")?.Value ?? string.Empty; // Assuming user ID is stored in JWT token

        var metricModel = new Metric(metric.DeviceId, DateTime.UtcNow, metric.Payload);

        var response = await _dispatcher.DispatchCreateMetricRequestAsync(userId, metricModel, cancellationToken);

        if (response is null)
        {
            _logger.LogError("Failed to create metric for device {DeviceId}", metric.DeviceId);

            return BadRequest("Failed to create metric.");
        }

        return CreatedAtAction(nameof(GetMetricsByDeviceIdAsync), new { deviceId = metric.DeviceId }, metric);
    }
}
