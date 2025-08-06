using HyperMsg.Messaging;
using HyperMsg.Scada.Shared.Models;
using Microsoft.AspNetCore.Http;
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
    [ProducesResponseType(typeof(IEnumerable<DeviceType>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        //var response = await _dispatcher.DispatchDeviceTypeListRequestAsync(cancellationToken);
        return Ok();// response);
    }
}
