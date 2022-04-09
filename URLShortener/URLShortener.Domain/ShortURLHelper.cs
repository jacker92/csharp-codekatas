namespace URLShortener.Domain
{
    public class ShortURLHelper
    {
        public static bool IsShortUrl(string url, string baseUrl)
        {
            return url.StartsWith(baseUrl);
        }
        public static string GenerateShortenedUrl(string baseUrl)
        {
            return baseUrl + RandomStringGenerator.Generate(7);
        }
        public static void Validate(string url)
        {
            _ = new Uri(url);
        }
    }
}