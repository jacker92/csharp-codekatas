namespace URLShortener.Domain
{
    public class UrlStatistics
    {
        public object LongUrl { get; set; }
        public int TimesAccessed { get; internal set; }
        public string ShortUrl { get; internal set; }
    }
}