namespace URLShortener.Domain
{
    public class URLShortener
    {
        private const string _baseUrl = "https://short.url/";
        private readonly ShortURLRepository _shortUrlRepository;

        public URLShortener()
        {
            _shortUrlRepository = new ShortURLRepository();
        }

        public string GetShortUrl(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            Validate(url);

            return GetShortenedUrlForLongUrl(url);
        }

        public string Translate(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            Validate(url);

            if (!IsShortUrl(url))
            {
                return GetShortenedUrlForLongUrl(url);
            }

            return _shortUrlRepository.GetExistingEntryByShortenedUrl(url).ShortUrl;
        }

        public UrlStatistics GetStatistics(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            Validate(url);

            if (!_shortUrlRepository.Urls.ContainsKey(url))
            {
                throw new ShortenedUrlNotFoundException($"No statistics found for url: {url}");
            }

            return _shortUrlRepository.Urls[url];
        }

        private string GetShortenedUrlForLongUrl(string url)
        {
            if (_shortUrlRepository.Urls.ContainsKey(url))
            {
                _shortUrlRepository.Urls[url].TimesAccessed++;
                return _shortUrlRepository.Urls[url].ShortUrl;
            }

            var shortenedUrl = GenerateShortenedUrl();

            CreateUrlStatistics(url, shortenedUrl);

            return shortenedUrl;
        }

        private static bool IsShortUrl(string url)
        {
            return url.StartsWith(_baseUrl);
        }

        private void CreateUrlStatistics(string url, string shortenedUrl)
        {
            _shortUrlRepository.Urls[url] = new UrlStatistics
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