using Castle.Core.Logging;
using FakeItEasy;
using HyperMsg.Messaging;
using HyperMsg.Scada.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
    public async Task GetMetricsAsync_ShouldReturnOk()
    {
        // Arrange
        var from = DateTime.UtcNow.AddDays(-1);
        var to = DateTime.UtcNow;
        // Act
        var result = await _controller.GetMetricsAsync(from, to, CancellationToken.None);
        // Assert
        Assert.IsType<OkResult>(result);
    }
}
