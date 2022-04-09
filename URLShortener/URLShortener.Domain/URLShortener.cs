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

            Validate(url);
        }

        private static void Validate(string url)
        {
            _ = new Uri(url);
        }
    }
}