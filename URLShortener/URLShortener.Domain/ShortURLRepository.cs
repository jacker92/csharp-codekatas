namespace URLShortener.Domain
{
    public class ShortURLRepository
    {
        private readonly Dictionary<string, UrlStatistics> _urls;

        public ShortURLRepository()
        {
            _urls = new Dictionary<string, UrlStatistics>();
        }

        public Dictionary<string, UrlStatistics> Urls => _urls;

        public UrlStatistics GetExistingEntryByShortenedUrl(string shortenedUrl)
        {
            var correspondingUrl = Urls.SingleOrDefault(x => x.Value.ShortUrl == shortenedUrl).Value;
            if (correspondingUrl == null)
            {
                throw new ShortenedUrlNotFoundException($"No match found for short url: {shortenedUrl}");
            }

            return correspondingUrl;
        }
    }
}