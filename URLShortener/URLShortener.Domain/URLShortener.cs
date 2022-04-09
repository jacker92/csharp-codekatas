﻿namespace URLShortener.Domain
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
                _shortUrlRepository.IncrementAccessCount(url);
                return _shortUrlRepository.GetByLongUrl(url);
            }

            return _shortUrlRepository.CreateNewEntry(url, _baseUrl);
        }
    }
}