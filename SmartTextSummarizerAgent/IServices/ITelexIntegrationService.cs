namespace SmartTextSummarizerAgent.IServices
{
    public interface ITelexIntegrationService
    {
        string LoadIntegration();
        Task<string> SummarizeText(string text);
    }
}
