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
    }
}