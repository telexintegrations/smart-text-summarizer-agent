using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartTextSummarizerAgent.Dtos;
using SmartTextSummarizerAgent.IServices;
using SmartTextSummarizerAgent.Models.DTO;

namespace SmartTextSummarizerAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartAgentController : ControllerBase
    {
        private readonly IAIService _aiService;

        public SmartAgentController(IAIService aiService)
        {
            _aiService = aiService;
        }

        /// <summary>
        /// Enter text
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TextResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> ProcessSummary([FromBody] SummarizeTextDto summaryDto)
        {
            var summaryResponse = await _aiService.SummarizeText(summaryDto.Message);

            if (summaryResponse == null)
            {
                return BadRequest(summaryResponse);
            }
            return Ok(summaryResponse);
        }
    }
}

