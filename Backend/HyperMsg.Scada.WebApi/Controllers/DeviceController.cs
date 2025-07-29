using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HyperMsg.Scada.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet("{deviceId}")]
        public IActionResult GetById(string deviceId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create([FromBody]object device)
        {
            return CreatedAtAction(nameof(GetById), new { deviceId = "newDeviceId" }, device);
        }

        [HttpPut("{deviceId}")]
        public IActionResult Edit(string deviceId, [FromBody]object device)
        {
            return NoContent();
        }

        [HttpDelete("{deviceId}")]
        public ActionResult Delete(string deviceId)
        {
            return NoContent();
        }
    }
}
