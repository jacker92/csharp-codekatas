namespace URLShortener.Domain
{
    public class ShortenedUrlNotFoundException : Exception
    {
        public ShortenedUrlNotFoundException(string url) : base($"No statistics found for url: {url}")
        {
        }
    }
}