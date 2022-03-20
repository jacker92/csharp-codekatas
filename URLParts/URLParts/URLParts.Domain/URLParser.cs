namespace URLParts.Domain
{
    public class Url
    {
        private static readonly List<string> _protocols = new List<string> { "http", "https", "ftp", "sftp" };
        private static readonly List<string> _topLevelDomains = new List<string> { "fi", "com", "net", "org", "int", "edu", "gov", "mil" };

        public Url(string protocol, string subdomain, string domain)
        {
            if (!_protocols.Contains(protocol))
            {
                throw new FormatException();
            }

            Protocol = protocol;

            // empty string is allowed
            if (!string.IsNullOrEmpty(subdomain))
            {
                if (!char.IsLetter(subdomain.First()))
                {
                    throw new FormatException();
                }

                if (!subdomain.All(x => char.IsLetterOrDigit(x)))
                {
                    throw new FormatException();
                }
            }

            Subdomain = subdomain;

            if (!_topLevelDomains.Contains(domain.Split('.')[1]))
            {
                throw new FormatException();
            }

            if (!domain.Split('.')[0].All(x => char.IsLetterOrDigit(x)))
            {
                throw new FormatException();
            }

            Domain = domain;
        }

        public string Protocol { get; }
        public string Subdomain { get; }
        public string Domain { get; }
    }

    public class URLParser
    {
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

            return new Url(protocol, subdomain, domain);
        }
    }
}