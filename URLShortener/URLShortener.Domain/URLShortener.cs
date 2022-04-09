namespace URLShortener.Domain
{
    public class URLShortener
    {
        private const string _baseUrl = "https://short.url/";

        public string GetShortUrl(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            Validate(url);

            return GenerateShortenedUrl();
        }

        private static string GenerateShortenedUrl()
        {
            return _baseUrl + RandomStringGenerator.Generate(7);
        }

        private static void Validate(string url)
        {
            _ = new Uri(url);
        }
    }
}