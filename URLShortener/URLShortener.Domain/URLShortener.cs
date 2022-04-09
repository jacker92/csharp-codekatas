namespace URLShortener.Domain
{
    public class URLShortener
    {
        private const string _baseUrl = "https://short.url/";

        public string GetShortUrl(string url)
        {
            if (url is null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            Validate(url);

            return _baseUrl + RandomString(7);
        }

        private static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "abcedfghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static void Validate(string url)
        {
            _ = new Uri(url);
        }
    }
}