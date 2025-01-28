using ChatApp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HubController : ControllerBase
    {
        private readonly ISignalrConnection _signalRConnection;

        public HubController(ISignalrConnection signalRConnection)
        {
            _signalRConnection = signalRConnection; 
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string message)
        {
            var connect = _signalRConnection.StartConnection();
            await connect.InvokeAsync("SendMessageAll", "Admin", message);

            return Ok();
        }

    }
}
