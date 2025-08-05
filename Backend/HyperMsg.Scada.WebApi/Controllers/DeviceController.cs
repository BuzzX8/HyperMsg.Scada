using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Messages;
using HyperMsg.Scada.Shared.Models;
using HyperMsg.Scada.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HyperMsg.Scada.WebApi.Controllers;

/// <summary>
/// API for managing devices.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<DeviceController> _logger;

    public DeviceController(IDispatcher dispatcher, ILogger<DeviceController> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }

    /// <summary>
    /// Gets all devices.
    /// </summary>
    /// <returns>List of devices.</returns>
    [HttpGet]
    [EndpointDescription("Get all devices")]
    [ProducesResponseType(typeof(IEnumerable<DeviceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _dispatcher.DispatchDeviceListRequestAsync(cancellationToken);

        return Ok(response.Select(ToDeviceDto));
    }

    /// <summary>
    /// Gets a device by its ID.
    /// </summary>
    /// <param name="deviceId">The ID of the device.</param>
    /// <returns>The requested device.</returns>
    [HttpGet("{deviceId}")]
    [EndpointDescription("Get device by ID")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(DeviceDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(string deviceId, CancellationToken cancellationToken)
    {
        var response = await _dispatcher.DispatchDeviceRequestAsync(deviceId, cancellationToken);

        if (response is null || response.Id == string.Empty)
        {
            _logger.LogWarning("Device with ID {DeviceId} not found", deviceId);
            return NotFound(deviceId);
        }

        return Ok(ToDeviceDto(response!));
    }

    /// <summary>
    /// Creates a new device.
    /// </summary>
    /// <param name="device">The device to create.</param>
    /// <returns>The created device.</returns>
    [HttpPost]
    [EndpointDescription("Create a new device")]
    [ProducesResponseType(typeof(DeviceDto), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody]DeviceDto device)
    {
        return CreatedAtAction(nameof(GetById), new { deviceId = "newDeviceId" }, device);
    }

    /// <summary>
    /// Updates an existing device.
    /// </summary>
    /// <param name="deviceId">The ID of the device to update.</param>
    /// <param name="device">The updated device data.</param>
    /// <returns>No content.</returns>
    [HttpPut("{deviceId}")]
    [EndpointDescription("Update an existing device")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Edit(string deviceId, [FromBody]DeviceDto device)
    {
        return NoContent();
    }

    /// <summary>
    /// Deletes a device by its ID.
    /// </summary>
    /// <param name="deviceId">The ID of the device to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{deviceId}")]
    [EndpointDescription("Delete a device by ID")]
    public ActionResult Delete(string deviceId)
    {
        return NoContent();
    }

    private static DeviceDto ToDeviceDto(Device device)
    {
        return new()
        {
            Id = device.Id,
            Name = device.Name,
            Type = device.Type
        };
    }
}
