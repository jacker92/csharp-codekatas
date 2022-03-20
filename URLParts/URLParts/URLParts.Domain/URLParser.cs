namespace URLParts.Domain
{
    public class URLParser
    {
        public void Decompose(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"'{nameof(url)}' cannot be null or whitespace.", nameof(url));
            }

            throw new FormatException();
        }
    }
}