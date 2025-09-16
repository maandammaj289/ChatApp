using System.Threading;
using System.Threading.Tasks;

using ChatApp.Application.Devices.Commands;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ChatApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class DevicesController : ControllerBase
    {
        private readonly RegisterDeviceTokenCommandHandler _handler;
        public DevicesController(RegisterDeviceTokenCommandHandler handler) { _handler = handler; }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDeviceTokenCommand cmd, CancellationToken ct)
        { await _handler.Handle(cmd, ct); return NoContent(); }
    }
}