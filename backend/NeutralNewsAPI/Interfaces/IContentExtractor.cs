public interface IContentExtractor
{
    Task<string> ExtractTextAsync(string url);
}