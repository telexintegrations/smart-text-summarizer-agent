namespace SmartTextSummarizerAgent.IServices
{
    public interface IAIService
    {
        Task<string> SummarizeText(string text);
    }
}
