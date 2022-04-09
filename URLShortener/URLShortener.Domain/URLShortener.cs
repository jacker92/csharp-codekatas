namespace URLShortener.Domain
{
    public class URLShortener
    {
        private const string _baseUrl = "https://short.url/";
        private readonly Dictionary<string, string> _urls;

        public URLShortener()
        {
            _urls = new Dictionary<string, string>();
        }

        public string GetShortUrl(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (_urls.ContainsKey(url))
            {
                return _urls[url];
            }

            Validate(url);
            var shortenedUrl = GenerateShortenedUrl();

            SaveUrl(url, shortenedUrl);

            return shortenedUrl;
        }

        private void SaveUrl(string url, string shortenedUrl)
        {
            _urls[url] = shortenedUrl;
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