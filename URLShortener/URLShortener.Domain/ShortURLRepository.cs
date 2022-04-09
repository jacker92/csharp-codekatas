﻿namespace URLShortener.Domain
{
    public class ShortURLRepository
    {
        private readonly Dictionary<string, UrlStatistics> _urls;
        private readonly URLStatisticsFactory _urlStatisticsFactory;

        public ShortURLRepository()
        {
            _urls = new Dictionary<string, UrlStatistics>();
            _urlStatisticsFactory = new URLStatisticsFactory();
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

        public void CreateNewEntry(string url, string shortenedUrl)
        {
            Urls[url] =  _urlStatisticsFactory.Create(url, shortenedUrl);
        }
    }
}