namespace URLParts.Domain
{
    public class Url
    {
        public string Protocol { get; set; }
    }

    public class URLParser
    {
        private static readonly List<string> _protocols = new List<string> { "http" };

        public Url Decompose(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"'{nameof(url)}' cannot be null or whitespace.", nameof(url));
            }

            var protocol = url.Split(':').FirstOrDefault();
            if (!_protocols.Contains(protocol))
            {
                throw new FormatException();
            }

            return new Url
            {
                Protocol = protocol
            };
        }
    }
}