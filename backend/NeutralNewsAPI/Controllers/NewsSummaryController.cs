using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NeutralNewsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsSummaryController : ControllerBase
{
    private readonly string _apiKey;
    private readonly IOpenAiService _openAiService;
    private readonly IExtractorFactory _extractorFactory;

    public NewsSummaryController(IConfiguration configuration, IOpenAiService openAiService, IExtractorFactory extractorFactory)
    {
        _extractorFactory = extractorFactory ?? throw new ArgumentNullException(nameof(extractorFactory));
        _apiKey = configuration["OpenAI:ApiKey"] ?? throw new ArgumentNullException("API Key not found in configuration.");
        _openAiService = openAiService ?? throw new ArgumentNullException(nameof(openAiService));
    }

    public class NewsRequest
    {
        [Required]
        public required string Url { get; set; }
        public float? Temperature { get; set; }
        public int? MaxTokens { get; set; }
    }

    public class NewsResponse
    {
        public string? Summary { get; set; }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] NewsRequest request)
    {
        try
        {
            var extractor = _extractorFactory.GetExtractor(request.Url);
            var articleText = await extractor.ExtractTextAsync(request.Url);

            var prompt = $"Resuma a seguinte notícia de forma neutra, sem inclinar para nenhum viés ideológico:\n\n{articleText}";
            var summary = await _openAiService.GenerateSummaryAsync(prompt, request.Temperature ?? 0f, request.MaxTokens ?? 512);

            if (string.IsNullOrWhiteSpace(summary))
                return BadRequest("Failed to generate summary.");

            return Ok(summary);
        }
        catch (NotSupportedException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
