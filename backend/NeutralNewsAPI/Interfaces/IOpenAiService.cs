public interface IOpenAiService
{
    Task<string> GenerateSummaryAsync(string prompt, float temperature, int maxTokens = 512);
}