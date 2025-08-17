using FakeItEasy;
using HyperMsg.Messaging;
using HyperMsg.Scada.WebApi.Controllers;
using HyperMsg.Scada.WebApi.Models;
using HyperMsg.Scada.Shared.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HyperMsg.Scada.WebApi.Tests.Controllers;

public class MetricsControllerTests
{
    private readonly MessageBroker _messageBroker;
    private readonly ILogger<MetricsController> _logger;
    private readonly MetricsController _controller;

    public MetricsControllerTests()
    {
        _messageBroker = new MessageBroker();
        _logger = A.Fake<ILogger<MetricsController>>();
        _controller = new MetricsController(_messageBroker, _logger);
    }

    [Fact]
    public async Task GetMetricsAsync_ShouldReturnNotFoundObject()
    {
        // Arrange
        var from = DateTime.UtcNow.AddDays(-1);
        var to = DateTime.UtcNow;
        // Act
        var result = await _controller.GetMetricsAsync(from, to, CancellationToken.None);
        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetMetricsByDeviceIdAsync_ShouldReturnNotFoundObject()
    {
        // Arrange
        var deviceId = "test-device-id";
        var from = DateTime.UtcNow.AddDays(-1);
        var to = DateTime.UtcNow;
        // Act
        var result = await _controller.GetMetricsByDeviceIdAsync(deviceId, from, to, CancellationToken.None);
        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task CreateMetricAsync_ShouldReturnCreated()
    {
        // Arrange
        var metricId = Guid.NewGuid().ToString();
        var metric = new CreateMetricDto
        {
            DeviceId = "test-device-id",
            Payload = JsonSerializer.Deserialize<JsonObject>("{\"temperature\": 22.5, \"humidity\": 60}")!,
        };
        _messageBroker.RegisterCreateMetricRequestHandler((userId, metricModel) =>
        {
            return metricId;
        });
        // Act
        var result = await _controller.CreateMetricAsync(metric, CancellationToken.None);
        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
        var createdResult = result as CreatedAtActionResult;
        Assert.NotNull(createdResult);
        Assert.Equal("GetMetricsByDeviceIdAsync", createdResult.ActionName);
        Assert.Equal(metric.DeviceId, createdResult.RouteValues!["deviceId"]);
    }
}
