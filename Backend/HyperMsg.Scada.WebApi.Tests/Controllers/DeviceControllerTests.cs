using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using HyperMsg.Scada.WebApi.Controllers;
using HyperMsg.Messaging;
using FakeItEasy;
using HyperMsg.Scada.Shared.Models;
using HyperMsg.Scada.WebApi.Models;
using HyperMsg.Scada.Shared.Messages;

namespace HyperMsg.Scada.WebApi.Tests.Controllers;

public class DeviceControllerTests
{
    private readonly MessageBroker messageBroker;
    private readonly ILogger<DeviceController> _logger;
    private readonly DeviceController _controller;

    public DeviceControllerTests()
    {
        messageBroker = new();
        _logger = A.Fake<ILogger<DeviceController>>();
        _controller = new DeviceController(messageBroker, _logger);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithDeviceList()
    {
        var devices = new List<Device> { new() { Id = "1", Name = "Test", Type = "TypeA" } };
        messageBroker.RegisterDeviceListRequestHandler(_ => new(devices));

        var result = await _controller.GetAll(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDevices = Assert.IsAssignableFrom<IEnumerable<DeviceDto>>(okResult.Value);
        Assert.Single(returnedDevices);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenDeviceExists()
    {
        var device = new Device { Id = "1", Name = "Test", Type = "TypeA" };
        messageBroker.RegisterDeviceRequestHandler(_ => new(device));

        var result = await _controller.GetById("1", CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDevice = Assert.IsType<DeviceDto>(okResult.Value);
        Assert.Equal("1", returnedDevice.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenDeviceDoesNotExist()
    {
        messageBroker.RegisterDeviceRequestHandler(_ => new(null!));

        var result = await _controller.GetById("2", CancellationToken.None);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void Create_ReturnsCreatedAtAction()
    {
        var deviceDto = new DeviceDto { Id = "newDeviceId", Name = "New", Type = "TypeB" };

        var result = _controller.Create(deviceDto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(deviceDto, createdResult.Value);
    }

    [Fact]
    public void Edit_ReturnsNoContent()
    {
        var deviceDto = new DeviceDto { Id = "1", Name = "Edit", Type = "TypeC" };

        var result = _controller.Edit("1", deviceDto);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Delete_ReturnsNoContent()
    {
        var result = _controller.Delete("1");

        Assert.IsType<NoContentResult>(result);
    }
}
