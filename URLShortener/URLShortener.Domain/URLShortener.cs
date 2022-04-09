namespace URLShortener.Domain
{
    public class URLShortener
    {
        private const string _baseUrl = "https://short.url/";
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ShortURLRepository _shortUrlRepository;
        private readonly URLStatisticsFactory _urlStatisticsFactory;

        public URLShortener(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            _urlStatisticsFactory = new URLStatisticsFactory(_dateTimeProvider);
            _shortUrlRepository = new ShortURLRepository(_dateTimeProvider, _urlStatisticsFactory);
        }

        public string GetShortUrl(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            ShortURLHelper.Validate(url);

            return GetOrCreateShortenedUrlForLongUrl(url).ShortUrl;
        }

        public string Translate(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            ShortURLHelper.Validate(url);

            if (ShortURLHelper.IsShortUrl(url, _baseUrl))
            {
                return _shortUrlRepository.GetByShortenedUrl(url).ShortUrl;
            }

            return GetOrCreateShortenedUrlForLongUrl(url).ShortUrl;
        }

        public UrlStatistics GetStatistics(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            ShortURLHelper.Validate(url);

            if (ShortURLHelper.IsShortUrl(url, _baseUrl))
            {
                return _shortUrlRepository.GetByShortenedUrl(url);
            }

            return _shortUrlRepository.GetByLongUrl(url);
        }

        private UrlStatistics GetOrCreateShortenedUrlForLongUrl(string url)
        {
            if (_shortUrlRepository.ContainsByLongUrl(url))
            {
                return _shortUrlRepository.AccessByLongUrl(url);
            }

            return _shortUrlRepository.CreateNewEntry(url, _baseUrl);
        }
    }
}