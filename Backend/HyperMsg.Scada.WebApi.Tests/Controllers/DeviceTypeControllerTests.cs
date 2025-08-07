using FakeItEasy;
using HyperMsg.Scada.Shared.Messages;
using HyperMsg.Scada.WebApi.Controllers;
using HyperMsg.Scada.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Models;

namespace HyperMsg.Scada.WebApi.Tests.Controllers;

public class DeviceTypeControllerTests
{
    private readonly MessageBroker messageBroker;
    private readonly ILogger<DeviceTypeController> _logger;
    private readonly DeviceTypeController _controller;

    public DeviceTypeControllerTests()
    {
        messageBroker = new();
        _logger = A.Fake<ILogger<DeviceTypeController>>();
        _controller = new DeviceTypeController(messageBroker, _logger);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithDeviceTypeList()
    {
        List<DeviceType> deviceTypes =
        [
            new () { Id = "1", Name = "TypeA", Description = "Desc" }
        ];
        messageBroker.RegisterDeviceTypeListRequestHandler(_ => deviceTypes);

        var result = await _controller.GetAll(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTypes = Assert.IsAssignableFrom<IEnumerable<DeviceTypeDto>>(okResult.Value);
        Assert.Single(returnedTypes);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenDeviceTypeExists()
    {
        var deviceType = new DeviceType 
        { 
            Id = "1", 
            Name = "TypeA", 
            Description = "Desc" 
        };
        messageBroker.RegisterDeviceTypeRequestHandler((userId, deviceTypeId) => deviceType);

        var result = await _controller.GetById("1", CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedType = Assert.IsType<DeviceTypeDto>(okResult.Value);
        Assert.Equal("1", returnedType.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenDeviceTypeDoesNotExist()
    {
        messageBroker.RegisterDeviceTypeRequestHandler((userId, deviceTypeId) => null!);

        var result = await _controller.GetById("2", CancellationToken.None);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Create_Returns_CreatedAtActionResult()
    {
        var newDeviceTypeId = Guid.NewGuid().ToString();
        var createDto = new CreateDeviceTypeDto 
        { 
            Name = "NewType", 
            Description = "Desc" 
        };
        messageBroker.RegisterCreateDeviceTypeRequestHandler(type => newDeviceTypeId);

        var result = await _controller.Create(createDto, CancellationToken.None);

        var createdResult = result as CreatedAtActionResult;
        Assert.NotNull(createdResult);
        Assert.Equal("GetById", createdResult.ActionName);
        Assert.Equal(newDeviceTypeId, createdResult.RouteValues!["deviceTypeId"]);
    }

    [Fact]
    public async Task Update_Returns_NoContent()
    {
        var deviceTypeDto = new DeviceTypeDto 
        { 
            Id = "1", 
            Name = "EditType", 
            Description = "Desc" 
        };
        messageBroker.RegisterUpdateDeviceTypeRequestHandler((userId, deviceType) => Task.CompletedTask);

        var result = await _controller.Update("1", deviceTypeDto, CancellationToken.None);

        Assert.IsType<NoContentResult>(result);
    }
}
