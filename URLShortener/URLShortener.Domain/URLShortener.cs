namespace URLShortener.Domain
{
    public class URLShortener
    {
        public void GetShortUrl(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            new Uri(url);
        }
    }
}