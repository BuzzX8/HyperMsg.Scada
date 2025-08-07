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
        var devices = new List<Device> 
        { 
            new() { Id = "1", Name = "Test", Type = "TypeA" } 
        };
        messageBroker.RegisterDeviceListRequestHandler(_ => devices);

        var result = await _controller.GetAll(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDevices = Assert.IsAssignableFrom<IEnumerable<DeviceDto>>(okResult.Value);
        Assert.Single(returnedDevices);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenDeviceExists()
    {
        var device = new Device { Id = "1", Name = "Test", Type = "TypeA" };
        messageBroker.RegisterDeviceRequestHandler((_, _) => device);

        var result = await _controller.GetById("1", CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDevice = Assert.IsType<DeviceDto>(okResult.Value);
        Assert.Equal("1", returnedDevice.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenDeviceDoesNotExist()
    {
        messageBroker.RegisterDeviceRequestHandler((_, _) => null!);

        var result = await _controller.GetById("2", CancellationToken.None);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        var newDeviceId = Guid.NewGuid().ToString();
        var deviceDto = new CreateDeviceDto 
        {
            Name = "New", 
            Type = "TypeB" 
        };
        messageBroker.RegisterCreateDeviceRequestHandler((_, device) => 
        {
            Assert.Equal(deviceDto.Name, device.Name);
            Assert.Equal(deviceDto.Type, device.Type);

            return newDeviceId;
        });

        var result = await _controller.Create(deviceDto, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(newDeviceId, createdResult.RouteValues!["deviceId"]);
    }

    [Fact]
    public async Task Update_ReturnsNoContent()
    {
        var deviceDto = new DeviceDto 
        { 
            Id = Guid.NewGuid().ToString(),
            Name = "Edit", 
            Type = "TypeC" 
        };
        messageBroker.RegisterUpdateDeviceRequestHandler((_, device) => 
        {
            Assert.Equal(deviceDto.Id, device.Id);
            Assert.Equal(deviceDto.Name, device.Name);
            Assert.Equal(deviceDto.Type, device.Type);
        });

        var result = await _controller.Update(deviceDto.Id, deviceDto, CancellationToken.None);

        Assert.IsType<NoContentResult>(result);
    }
}
