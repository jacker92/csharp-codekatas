namespace URLShortener.Domain
{
    public class ShortenedUrlNotFoundException : Exception
    {
        public ShortenedUrlNotFoundException(string message) : base(message)
        {
        }
    }
}