namespace URLShortener.Domain
{
    public class URLShortenerService
    {
        private const string _baseUrl = "https://short.url/";
        private readonly ShortURLRepository _shortUrlRepository;

        public URLShortenerService(ShortURLRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public string GetShortUrl(string url)
        {
            ShortURLHelper.Validate(url);

            return GetOrCreateShortenedUrlForLongUrl(url).ShortUrl;
        }

        public string Translate(string url)
        {
            ShortURLHelper.Validate(url);

            if (ShortURLHelper.IsShortUrl(url, _baseUrl))
            {
                return _shortUrlRepository.GetByShortenedUrl(url).ShortUrl;
            }

            return GetOrCreateShortenedUrlForLongUrl(url).ShortUrl;
        }

        public UrlStatistics GetStatistics(string url)
        {
            ShortURLHelper.Validate(url);

            if (ShortURLHelper.IsShortUrl(url, _baseUrl))
            {
                return _shortUrlRepository.GetByShortenedUrl(url);
            }

            return _shortUrlRepository.GetByLongUrl(url);
        }

        public string GenerateLog(string url)
        {
            ShortURLHelper.Validate(url);

            var statistics = GetStatistics(url);

            return LogOutputGeneratorService.Generate(statistics);
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