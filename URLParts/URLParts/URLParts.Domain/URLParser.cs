namespace URLParts.Domain
{
    public class Protocol
    {
        public string ProtocolName { get; set; }
        public int DefaultPort { get; set; }
    }

    public class Url
    {
        private static readonly List<Protocol> _protocols = new List<Protocol> {
            new Protocol { ProtocolName = "http", DefaultPort = 80},
            new Protocol { ProtocolName = "https", DefaultPort = 443},
            new Protocol { ProtocolName = "ftp", DefaultPort = 21},
            new Protocol { ProtocolName = "sftp", DefaultPort = 22} };

        private static readonly List<string> _topLevelDomains = new List<string> { "fi", "com", "net", "org", "int", "edu", "gov", "mil" };

        public Url(string protocol, string subdomain, string domain, int? port = null)
        {
            var correspondingProtocol = _protocols.SingleOrDefault(x => x.ProtocolName == protocol);

            if (correspondingProtocol == null)
            {
                throw new FormatException();
            }

            SetPort(port, correspondingProtocol);
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

        private void SetPort(int? port, Protocol correspondingProtocol)
        {
            if (port == null)
            {
                Port = correspondingProtocol.DefaultPort;
                return;
            }

            if (port.Value < 1 || port.Value > 65535)
            {
                throw new FormatException();
            }

            Port = port.Value;
        }

        public string Protocol { get; }
        public string Subdomain { get; }
        public string Domain { get; }
        public int Port { get; set; }
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

            var domainWithPossiblePortSplitted = domain.Split(':');

            if (domainWithPossiblePortSplitted.Length == 1)
            {
                return new Url(protocol, subdomain, domainWithPossiblePortSplitted[0]);
            }

            if (!int.TryParse(domainWithPossiblePortSplitted[1], out int port))
            {
                throw new FormatException();
            }

            return new Url(protocol, subdomain, domainWithPossiblePortSplitted[0], port);
        }
    }
}