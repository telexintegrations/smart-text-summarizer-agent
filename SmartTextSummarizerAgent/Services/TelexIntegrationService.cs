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



    }

}

