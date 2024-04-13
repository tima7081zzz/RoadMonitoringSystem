using System.Threading.Tasks;
using Agent.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Services.Interfaces;

namespace Store.Controllers
{
    [ApiController]
    [Route("api/processed-agent-data")]
    public class AgentDataController : ControllerBase
    {
        private readonly IProcessedAgentDataService _processedAgentDataService;

        public AgentDataController(IProcessedAgentDataService processedAgentDataService)
        {
            _processedAgentDataService = processedAgentDataService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var data = await _processedAgentDataService.Get(id);
                return Ok(data);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _processedAgentDataService.Delete(id);
                return Ok();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProcessedAgentDataRequestModel data)
        {
            try
            {
                await _processedAgentDataService.Update(id, data);
                return Ok(data);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProcessedAgentDataRequestModel data)
        {
            var added = await _processedAgentDataService.Add(data);
            return Ok(added);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> BulkAdd([FromBody] BulkAddAgentDatasRequestModel model)
        {
            await _processedAgentDataService.BulkAdd(model.Models);
            return Ok();
        }
    }
}