using HtmlAgilityPack;

public class UOLExtractor : IContentExtractor
{
    public async Task<string> ExtractTextAsync(string url)
    {
        using var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(url);

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var articleNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'article-text') or contains(@class,'content-text')]");

        if (articleNode == null)
            return string.Empty;

        var paragraphs = articleNode.SelectNodes(".//p");
        if (paragraphs == null)
            return string.Empty;

        return string.Join("\n\n", paragraphs.Select(p => p.InnerText.Trim()));
    }
}