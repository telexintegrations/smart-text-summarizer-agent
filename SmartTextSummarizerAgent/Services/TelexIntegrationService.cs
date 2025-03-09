        using System;
using System.Net.Http;
using System.Threading.Tasks;
using SmartTextSummarizerAgent.IServices;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using SmartTextSummarizerAgent.Helpers;

namespace SmartTextSummarizerAgent.Services
{
    public class TelexIntegrationService : ITelexIntegrationService
    {
        private string _configurationFilePath = Path.Combine(Directory.GetCurrentDirectory(), "integration.json");
        private readonly HttpClient _httpClient;
        private readonly string _apiKey; 

        public TelexIntegrationService(IOptions<GeminiSettings> geminiSettings)
        {
            _httpClient = new HttpClient();
            _apiKey = geminiSettings.Value.ApiKey;
        }

        public string LoadIntegration()
        {
            if (!File.Exists(_configurationFilePath))
            {
                throw new FileNotFoundException("The configuration file was not found.");
            }

            var json = File.ReadAllText(_configurationFilePath);
            using JsonDocument doc = JsonDocument.Parse(json);
            return JsonSerializer.Serialize(doc.RootElement);

        }



        public async Task<string> SummarizeText(string text)
        {
            var requestBody = new
            {
                contents = new[]
                {
                new { role = "user", parts = new[] { new { text = $"Improve this resume: {text} in Igbo" } } }
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

