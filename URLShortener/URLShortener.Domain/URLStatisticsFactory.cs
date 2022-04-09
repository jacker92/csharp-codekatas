namespace URLShortener.Domain
{
    public class URLStatisticsFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        public URLStatisticsFactory(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public UrlStatistics Create(string longUrl, string shortenedUrl)
        {
            return new UrlStatistics
            {
                LongUrl = longUrl,
                ShortUrl = shortenedUrl,
                TimesAccessed = new List<LogEntry>() { new LogEntry { Timestamp = _dateTimeProvider.DateTimeNow } }
            };
        }
    }
}