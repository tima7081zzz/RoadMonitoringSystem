using System.Threading.Tasks;
using Hub.Models;
using Hub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Controllers
{
    [ApiController]
    [Route("api/processed-agent-data")]
    public class HubController : ControllerBase
    {
        private readonly IAgentDataSaverService _saverService;

        public HubController(IAgentDataSaverService saverService)
        {
            _saverService = saverService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProcessedAgentData data)
        {
            await _saverService.Save(data);
            return Ok();
        }
    }
}