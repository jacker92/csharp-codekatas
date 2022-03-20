namespace URLParts.Domain
{
    public class Url
    {
        public string Protocol { get; set; }
        public string Subdomain { get; set; }
        public IEnumerable<char> Domain { get; set; }
    }

    public class URLParser
    {
        private static readonly List<string> _protocols = new List<string> { "http", "https", "ftp", "sftp" };
        private static readonly List<string> _topLevelDomains = new List<string> { "fi", "com", "net", "org", "int", "edu", "gov", "mil" };

        public Url Decompose(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"'{nameof(url)}' cannot be null or whitespace.", nameof(url));
            }

            if (!url.Contains(":") || !url.Contains("//") || !url.Contains("."))
            {
                throw new FormatException();
            }

            var protocol = url.Split(':').FirstOrDefault();

            if (string.IsNullOrWhiteSpace(protocol))
            {
                throw new FormatException();
            }
            if (!_protocols.Contains(protocol))
            {
                throw new FormatException();
            }

            var domainAndRest = url.Split("//")[1];

            var domains = domainAndRest.Split('.');

            if (domains.Any(domain => string.IsNullOrWhiteSpace(domain)))
            {
                throw new FormatException();
            }

            var subdomain = string.Empty;
            var domain = domainAndRest;
            if (domains.Count() > 2)
            {
                subdomain = domains[0];
                domain = domainAndRest.Split('.', 2)[1];
            }

            var toplevelDomain = domain.Split(".")[1];

            if (!_topLevelDomains.Contains(toplevelDomain))
            {
                throw new FormatException();
            }

            return new Url
            {
                Protocol = protocol,
                Domain = domain,
                Subdomain = subdomain
            };
        }
    }
}