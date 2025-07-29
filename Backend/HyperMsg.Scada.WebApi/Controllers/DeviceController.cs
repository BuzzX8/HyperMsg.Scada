using HyperMsg.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace HyperMsg.Scada.WebApi.Controllers;

/// <summary>
/// API for managing devices.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly IMessagingContext _messagingContext;
    private readonly ILogger<DeviceController> _logger;

    public DeviceController(IMessagingContext messagingContext, ILogger<DeviceController> logger)
    {
        _messagingContext = messagingContext;
        _logger = logger;
    }

    /// <summary>
    /// Gets all devices.
    /// </summary>
    /// <returns>List of devices.</returns>
    [HttpGet]
    [EndpointDescription("Get all devices")]
    public IActionResult GetAll()
    {
        return Ok();
    }

    /// <summary>
    /// Gets a device by its ID.
    /// </summary>
    /// <param name="deviceId">The ID of the device.</param>
    /// <returns>The requested device.</returns>
    [HttpGet("{deviceId}")]
    [EndpointDescription("Get device by ID")]
    public IActionResult GetById(string deviceId)
    {
        return Ok();
    }

    /// <summary>
    /// Creates a new device.
    /// </summary>
    /// <param name="device">The device to create.</param>
    /// <returns>The created device.</returns>
    [HttpPost]
    [EndpointDescription("Create a new device")]
    public IActionResult Create([FromBody]object device)
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
    public IActionResult Edit(string deviceId, [FromBody]object device)
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
}
