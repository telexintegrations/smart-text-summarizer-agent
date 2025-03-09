using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartTextSummarizerAgent.IServices;

namespace SmartTextSummarizerAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelexIntegrationController : ControllerBase
    {
        private readonly ITelexIntegrationService _loader;
        public TelexIntegrationController(ITelexIntegrationService loader)
        {
            _loader = loader; 
        }


        [HttpGet]
        public IActionResult GetIntegrationConfig()
        {
            
            var integrationJson = _loader.LoadIntegration();

            if (integrationJson == null)
            {
                return NotFound();
            }

            return Ok(integrationJson);
        }

        [HttpPost]
        public async Task<IActionResult> PostIntegrationConfig([FromBody] string text)
        {
            var jsonResponse = await _loader.SummarizeText(text);

            if (jsonResponse == null)
            {
                return BadRequest(jsonResponse);
            }
            return Ok(jsonResponse);
        }
    }
}
