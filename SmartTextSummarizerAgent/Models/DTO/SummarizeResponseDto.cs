using System.Text.Json.Serialization;

namespace SmartTextSummarizerAgent.Models.DTO
{
    public class TextResponseDto
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
