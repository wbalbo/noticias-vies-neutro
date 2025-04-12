public interface IExtractorFactory
{
    IContentExtractor GetExtractor(string url);
}

public class ExtractorFactory : IExtractorFactory
{
    private readonly G1Extractor _g1Extractor;
    private readonly UOLExtractor _uolExtractor;

    public ExtractorFactory(G1Extractor g1Extractor, UOLExtractor uolExtractor)
    {
        _g1Extractor = g1Extractor ?? throw new ArgumentNullException(nameof(g1Extractor));
        _uolExtractor = uolExtractor ?? throw new ArgumentNullException(nameof(uolExtractor));
    }

    public IContentExtractor GetExtractor(string url)
    {
        if (url.Contains("g1.globo.com", StringComparison.OrdinalIgnoreCase))
            return _g1Extractor;

        // TODO: Not working correctly, need to implement a Web Scraping solution like Playwright or Selenium
        // if (url.Contains("uol.com.br", StringComparison.OrdinalIgnoreCase))
        //     return _uolExtractor;

        throw new NotSupportedException("Site not supported for extraction.");
    }
}