using SmartTextSummarizerAgent.IServices;

namespace SmartTextSummarizerAgent.Services
{
    public class AIService : IAIService
    {
        public Task<string?> SummarizeText(string text)
        {
            return Task.FromResult<string?>("This is the summarized text");
        }
    }
}
