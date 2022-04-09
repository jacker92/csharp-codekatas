namespace URLShortener.Domain
{
    public class ShortURLRepository
    {
        private readonly Dictionary<string, UrlStatistics> _urls;
        private readonly URLStatisticsFactory _urlStatisticsFactory;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ShortURLRepository(IDateTimeProvider dateTimeProvider)
        {
            _urls = new Dictionary<string, UrlStatistics>();
            _dateTimeProvider = dateTimeProvider;
            _urlStatisticsFactory = new URLStatisticsFactory(_dateTimeProvider);
        }

        public Dictionary<string, UrlStatistics> Urls => _urls;

        public UrlStatistics GetByShortenedUrl(string shortenedUrl)
        {
            var correspondingUrl = Urls.SingleOrDefault(x => x.Value.ShortUrl == shortenedUrl).Value;
            if (correspondingUrl == null)
            {
                throw new ShortenedUrlNotFoundException($"No match found for short url: {shortenedUrl}");
            }

            return correspondingUrl;
        }

        public UrlStatistics CreateNewEntry(string url, string baseUrl)
        {
            var shortenedUrl = ShortURLHelper.GenerateShortenedUrl(baseUrl);
            Urls[url] = _urlStatisticsFactory.Create(url, shortenedUrl);
            return Urls[url];
        }

        public UrlStatistics GetByLongUrl(string url)
        {
            if (!ContainsByLongUrl(url))
            {
                throw new ShortenedUrlNotFoundException($"No statistics found for url: {url}");
            }

            return Urls[url];
        }

        public UrlStatistics AccessByLongUrl(string url)
        {
            var result = GetByLongUrl(url);
            Urls[url].TimesAccessed.Add(new LogEntry { Timestamp = _dateTimeProvider.DateTimeNow });
            return result;
        }

        public bool ContainsByLongUrl(string url)
        {
            return Urls.ContainsKey(url);
        }
    }
}