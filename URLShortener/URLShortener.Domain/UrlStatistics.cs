namespace URLShortener.Domain
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
    }

    public class UrlStatistics
    {
        public string LongUrl { get; set; }
        public IList<LogEntry> TimesAccessed { get; set; }
        public string ShortUrl { get; set; }
    }
}