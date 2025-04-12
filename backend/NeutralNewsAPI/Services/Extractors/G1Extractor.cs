using HtmlAgilityPack;

public class G1Extractor : IContentExtractor
{
    public async Task<string> ExtractTextAsync(string url)
    {
        var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(url);

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var articleNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'mc-article-body')]");

        if (articleNode == null)
            return string.Empty;

        var paragraphs = articleNode.SelectNodes(".//p");
        if (paragraphs == null)
            return string.Empty;

        return string.Join("\n\n", paragraphs.Select(p => p.InnerText.Trim()));
    }
}