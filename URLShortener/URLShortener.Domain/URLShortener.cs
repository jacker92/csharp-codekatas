namespace URLShortener.Domain
{
    public class URLShortener
    {
        public string GetShortUrl(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            Validate(url);

            return "https://short.url/";
        }

        private static void Validate(string url)
        {
            _ = new Uri(url);
        }
    }
}