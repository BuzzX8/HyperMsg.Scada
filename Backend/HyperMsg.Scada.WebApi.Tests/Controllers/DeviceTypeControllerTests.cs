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
        var deviceType = new DeviceTypeDto { Id = "1", Name = "TypeA", Description = "Desc" };
        //A.CallTo(() => _dispatcher.DispatchDeviceTypeRequestAsync("1", A<CancellationToken>._))
        //    .Returns(deviceType);

        var result = await _controller.GetById("1", CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedType = Assert.IsType<DeviceTypeDto>(okResult.Value);
        Assert.Equal("1", returnedType.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenDeviceTypeDoesNotExist()
    {
        //A.CallTo(() => _dispatcher.DispatchDeviceTypeRequestAsync("2", A<CancellationToken>._))
        //    .Returns((DeviceTypeDto)null);

        var result = await _controller.GetById("2", CancellationToken.None);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsOk()
    {
        var createDto = new CreateDeviceTypeDto 
        { 
            Name = "NewType", 
            Description = "Desc" 
        };

        var result = await _controller.Create(createDto, CancellationToken.None);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsOk()
    {
        var deviceTypeDto = new DeviceTypeDto { Id = "1", Name = "EditType", Description = "Desc" };

        var result = await _controller.Update("1", deviceTypeDto, CancellationToken.None);

        Assert.IsType<OkResult>(result);
    }
}
