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

            ShortURLHelper.Validate(url);

            return GetShortenedUrlForLongUrl(url);
        }

        public string Translate(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            ShortURLHelper.Validate(url);

            if (!ShortURLHelper.IsShortUrl(url, _baseUrl))
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

            ShortURLHelper.Validate(url);

            if (!_shortUrlRepository.ContainsByLongUrl(url))
            {
                throw new ShortenedUrlNotFoundException($"No statistics found for url: {url}");
            }

            return _shortUrlRepository.GetByLongUrl(url);
        }

        private string GetShortenedUrlForLongUrl(string url)
        {
            if (_shortUrlRepository.ContainsByLongUrl(url))
            {
                _shortUrlRepository.Urls[url].TimesAccessed++;
                return _shortUrlRepository.Urls[url].ShortUrl;
            }

            var shortenedUrl = ShortURLHelper.GenerateShortenedUrl(_baseUrl);

            _shortUrlRepository.CreateNewEntry(url, shortenedUrl);

            return shortenedUrl;
        }
    }
}