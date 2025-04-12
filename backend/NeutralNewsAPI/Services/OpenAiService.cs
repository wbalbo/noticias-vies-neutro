using NeutralNewsAPI.Utils;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

public class OpenAiService : IOpenAiService
{
    private readonly OpenAIService _client;

    public OpenAiService(OpenAIService client)
    {
        _client = client;
    }

    public async Task<string> GenerateSummaryAsync(string prompt, float temperature, int maxTokens = 512)
    {
        var result = await _client.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages =
           [
               ChatMessage.FromSystem(PromptConstants.NeutralSystemMessage), ChatMessage.FromUser(prompt)
           ],
            Model = Models.Gpt_3_5_Turbo,
            Temperature = temperature,
            MaxTokens = maxTokens
        });

        if (!result.Successful)
            return "Error generating summary.";

        if (result.Choices.Count == 0)
            return "Error generating summary.";

        return result.Choices.First().Message?.Content?.Trim() ?? "Error: No content generated.";
    }
}