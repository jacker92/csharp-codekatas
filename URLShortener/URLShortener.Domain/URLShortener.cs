namespace URLShortener.Domain
{
    public class URLShortener
    {
        private const string _baseUrl = "https://short.url/";
        private readonly Dictionary<string, UrlStatistics> _urls;

        public URLShortener()
        {
            _urls = new Dictionary<string, UrlStatistics>();
        }

        public string GetShortUrl(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            Validate(url);

            return GetShortenedUrl(url);
        }

        public string Translate(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            Validate(url);

            if (IsShortUrl(url))
            {
                return url;
                //var correspondingUrl = _urls.SingleOrDefault(x => x.Value.ShortUrl == url).Value;
                //return correspondingUrl.ShortUrl;
            }

            return GetShortenedUrl(url);
        }

        public UrlStatistics GetStatistics(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            Validate(url);

            if (!_urls.ContainsKey(url))
            {
                throw new ShortenedUrlNotFoundException(url);
            }

            return _urls[url];
        }

        private string GetShortenedUrl(string url)
        {
            if (_urls.ContainsKey(url))
            {
                _urls[url].TimesAccessed++;
                return _urls[url].ShortUrl;
            }

            var shortenedUrl = GenerateShortenedUrl();

            SaveUrl(url, shortenedUrl);

            return shortenedUrl;
        }

        private static bool IsShortUrl(string url)
        {
            return url.StartsWith(_baseUrl);
        }

        private void SaveUrl(string url, string shortenedUrl)
        {
            _urls[url] = new UrlStatistics
            {
                LongUrl = url,
                ShortUrl = shortenedUrl,
                TimesAccessed = 1
            };
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