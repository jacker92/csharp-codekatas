namespace URLShortener.Domain
{
    public class URLShortener
    {
        private readonly URLShortenerService _urlShortenerService;

        public URLShortener(URLShortenerService urlShortenerService)
        {
            _urlShortenerService = urlShortenerService;
        }

        public string GetShortUrl(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            return _urlShortenerService.GetShortUrl(url);
        }

        public string Translate(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            return _urlShortenerService.Translate(url);
        }

        public UrlStatistics GetStatistics(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            return _urlShortenerService.GetStatistics(url);
        }
    }
}