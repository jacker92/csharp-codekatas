namespace URLShortener.Domain
{
    public class URLStatisticsFactory
    {
        public UrlStatistics Create(string longUrl, string shortenedUrl)
        {
            return new UrlStatistics
            {
                LongUrl = longUrl,
                ShortUrl = shortenedUrl,
                TimesAccessed = 1
            };
        }
    }
}