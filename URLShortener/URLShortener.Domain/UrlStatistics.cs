namespace URLShortener.Domain
{
    public class UrlStatistics
    {
        public string LongUrl { get; set; }
        public int TimesAccessed { get; set; }
        public string ShortUrl { get; set; }
    }
}