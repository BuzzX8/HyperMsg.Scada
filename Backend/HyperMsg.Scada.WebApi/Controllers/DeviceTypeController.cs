using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Messages;
using HyperMsg.Scada.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HyperMsg.Scada.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeviceTypeController : ControllerBase
{
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<DeviceTypeController> _logger;
    public DeviceTypeController(IDispatcher dispatcher, ILogger<DeviceTypeController> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }
    /// <summary>
    /// Gets all device types.
    /// </summary>
    /// <returns>List of device types.</returns>
    [HttpGet]
    [EndpointDescription("Get all device types")]
    [ProducesResponseType(typeof(IEnumerable<DeviceTypeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _dispatcher.DispatchDeviceTypeListRequestAsync(cancellationToken);

        return Ok(response);
    }

    [HttpGet("{deviceTypeId}")]
    [EndpointDescription("Get device type by ID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(DeviceTypeDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(string deviceTypeId, CancellationToken cancellationToken)
    {
        var response = await _dispatcher.DispatchDeviceTypeRequestAsync(deviceTypeId, cancellationToken);
        
        if (response is null || response.Id == string.Empty)
        {
            _logger.LogWarning("Device type with ID {DeviceTypeId} not found", deviceTypeId);
            return NotFound(deviceTypeId);
        }

        return Ok(ToDeviceTypeDto(response!));
    }

    [HttpPost]
    [EndpointDescription("Create a new device type")]
    [ProducesResponseType(typeof(DeviceTypeDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(DeviceTypeDto deviceTypeDto, CancellationToken cancellationToken)
    {
        //var deviceType = new HyperMsg.Scada.Shared.Models.DeviceType
        //{
        //    Id = deviceTypeDto.Id,
        //    Name = deviceTypeDto.Name,
        //    Description = deviceTypeDto.Description,
        //    //MetricTemplates = deviceTypeDto.MetricTemplates.Select(m => m.ToModel()),
        //    //CommandTemplates = deviceTypeDto.CommandTemplates.Select(c => c.ToModel())
        //};
        //var response = await _dispatcher.DispatchDeviceTypeCreateRequestAsync(deviceType, cancellationToken);
        return Ok();// ToDeviceTypeDto(response);
    }

    [HttpPut("{deviceTypeId}")]
    [EndpointDescription("Update an existing device type")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(DeviceTypeDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(string deviceTypeId, DeviceTypeDto deviceTypeDto, CancellationToken cancellationToken)
    {
        //var deviceType = new HyperMsg.Scada.Shared.Models.DeviceType
        //{
        //    Id = deviceTypeId,
        //    Name = deviceTypeDto.Name,
        //    Description = deviceTypeDto.Description,
        //    //MetricTemplates = deviceTypeDto.MetricTemplates.Select(m => m.ToModel()),
        //    //CommandTemplates = deviceTypeDto.CommandTemplates.Select(c => c.ToModel())
        //};
        //var response = await _dispatcher.DispatchDeviceTypeUpdateRequestAsync(deviceType, cancellationToken);
        return Ok();// ToDeviceTypeDto(response);
    }

    public static DeviceTypeDto ToDeviceTypeDto(HyperMsg.Scada.Shared.Models.DeviceType deviceType)
    {
        return new DeviceTypeDto
        {
            Id = deviceType.Id,
            Name = deviceType.Name,
            Description = deviceType.Description,
            //MetricTemplates = deviceType.MetricTemplates.Select(m => m.ToJSObject()),
            //CommandTemplates = deviceType.CommandTemplates.Select(c => c.ToJSObject())
        };
    }
}
