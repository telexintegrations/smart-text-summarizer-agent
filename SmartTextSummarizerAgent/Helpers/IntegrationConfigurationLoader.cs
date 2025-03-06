using Newtonsoft.Json;

namespace SmartTextSummarizerAgent.Helpers
{
    public class IntegrationConfigurationLoader
    {
        private string _configurationFilePath = "integration.json";

        public IntegrationConfigurationLoader()
        {
            if (File.Exists(_configurationFilePath))
            {
                var json = File.ReadAllText(_configurationFilePath);
                var configuration = JsonConvert.DeserializeObject<IntegrationConfiguration>(json);
                Configuration = configuration;
            }
            else
            {
                Configuration = new IntegrationConfiguration();
            }
        }
    }
}
