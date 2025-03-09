namespace SmartTextSummarizerAgent.Dtos
{
    public class SummarizeTextDto
    {
        public string Message {  get; set; }
        public List<Setting>? settings { get; set; }
    }
}
