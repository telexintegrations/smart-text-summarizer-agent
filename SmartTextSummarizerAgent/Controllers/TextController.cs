using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartTextSummarizerAgent.IServices;
using SmartTextSummarizerAgent.Models.DTO;

namespace SmartTextSummarizerAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly IAIService aiService;

        public TextController(IAIService aiService)
        {
            this.aiService = aiService;
        }

        /// <summary>
        /// Enter text
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TextResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<TextResponseDto>> EnterText([FromBody] SummarizeRequestDto textRequestDto)
        {
            string? summarizedText = await aiService.SummarizeText(textRequestDto.Text);

            if (summarizedText == null) {
                return BadRequest();
            }

            var textResponseDto = new TextResponseDto
            {
                Text = summarizedText,
            };

            return StatusCode(StatusCodes.Status200OK, textResponseDto);
        }
    }
}

