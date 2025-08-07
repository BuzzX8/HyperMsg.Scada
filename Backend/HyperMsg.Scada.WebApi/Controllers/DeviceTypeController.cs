using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Messages;
using HyperMsg.Scada.Shared.Models;
using HyperMsg.Scada.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HyperMsg.Scada.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
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

        return Ok(response.Select(ToDeviceTypeDto));
    }

    [HttpGet("{deviceTypeId}")]
    [EndpointDescription("Get device type by ID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(DeviceTypeDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(string deviceTypeId, CancellationToken cancellationToken)
    {
        var response = await _dispatcher.DispatchDeviceTypeRequestAsync(deviceTypeId, cancellationToken);
        
        if (response is null)
        {
            _logger.LogWarning("Device type with ID {DeviceTypeId} not found", deviceTypeId);
            return NotFound(deviceTypeId);
        }

        return Ok(ToDeviceTypeDto(response!));
    }

    [HttpPost]
    [EndpointDescription("Create a new device type")]
    [ProducesResponseType(typeof(DeviceTypeDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateDeviceTypeDto createDto, CancellationToken cancellationToken)
    {
        var deviceType = new DeviceType
        {
            Name = createDto.Name,
            Description = createDto.Description
        };

        var response = await _dispatcher.DispatchCreateDeviceTypeRequestAsync("", deviceType, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { deviceTypeId = response }, deviceType);
    }

    [HttpPut("{deviceTypeId}")]
    [EndpointDescription("Update an existing device type")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(DeviceTypeDto), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(string deviceTypeId, DeviceTypeDto deviceTypeDto, CancellationToken cancellationToken)
    {
        DeviceType deviceType = new()
        {
            Id = deviceTypeId,
            Name = deviceTypeDto.Name,
            Description = deviceTypeDto.Description
        };

        await _dispatcher.DispatchUpdateDeviceTypeRequestAsync("", deviceType, cancellationToken);

        return NoContent();
    }

    public static DeviceTypeDto ToDeviceTypeDto(DeviceType deviceType)
    {
        return new()
        {
            Id = deviceType.Id,
            Name = deviceType.Name,
            Description = deviceType.Description
        };
    }
}
