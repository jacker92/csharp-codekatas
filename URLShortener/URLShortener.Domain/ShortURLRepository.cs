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

        public UrlStatistics GetByShortenedUrl(string shortenedUrl)
        {
            var correspondingUrl = Urls.SingleOrDefault(x => x.Value.ShortUrl == shortenedUrl).Value;
            if (correspondingUrl == null)
            {
                throw new ShortenedUrlNotFoundException($"No match found for short url: {shortenedUrl}");
            }

            return correspondingUrl;
        }

        public UrlStatistics AccessUrl(string url)
        {
            Urls[url].TimesAccessed++;
            return Urls[url];
        }

        public UrlStatistics CreateNewEntry(string url, string shortenedUrl)
        {
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

        public bool ContainsByLongUrl(string url)
        {
            return Urls.ContainsKey(url);
        }
    }
}