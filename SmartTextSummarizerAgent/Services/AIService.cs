using SmartTextSummarizerAgent.IServices;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Options;
using SmartTextSummarizerAgent.Helpers;

namespace SmartTextSummarizerAgent.Services
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AIService(IOptions<GeminiSettings> geminiSettings)
        {
            _httpClient = new HttpClient();
            _apiKey = geminiSettings.Value.ApiKey;
        }

        public async Task<string> SummarizeText(string text)
        {
            var requestBody = new
            {
                contents = new[]
                {
                new { role = "user", parts = new[] { new { text = $"Summarize this text: {text}" } } }
            }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://generativelanguage.googleapis.com/v1/models/gemini-2.0-flash:generateContent?key={_apiKey}", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var responseJson = JsonSerializer.Deserialize<JsonElement>(responseString);

            return responseJson.GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();
        }
    }
}
