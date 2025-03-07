using System.Text.Json.Serialization;

namespace SmartTextSummarizerAgent.Models.DTO
{
    public class TextRequestDto
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
