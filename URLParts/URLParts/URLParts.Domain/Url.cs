namespace URLParts.Domain
{
    public class Url
    {
        private static readonly List<Protocol> _protocols = new List<Protocol> {
            new Protocol { ProtocolName = "http", DefaultPort = 80},
            new Protocol { ProtocolName = "https", DefaultPort = 443},
            new Protocol { ProtocolName = "ftp", DefaultPort = 21},
            new Protocol { ProtocolName = "sftp", DefaultPort = 22} };

        private static readonly List<string> _topLevelDomains = new List<string> { "fi", "com", "net", "org", "int", "edu", "gov", "mil" };

        public Url(string protocol, string subdomain, string domain, int? port, string path, string query, string anchor)
        {
            var correspondingProtocol = _protocols.SingleOrDefault(x => x.ProtocolName == protocol);

            ValidateProtocol(correspondingProtocol);
            ValidatePort(port);

            Port = GetPort(port, correspondingProtocol!);

            Protocol = protocol;

            ValidateSubdomain(subdomain);

            Subdomain = subdomain;

            ValidateDomain(domain);

            Domain = domain;

            ValidatePath(path);

            Path = path;

            ValidateQuery(query);

            Query = query;

            ValidateAnchor(anchor);

            Anchor = anchor;
        }

        private int GetPort(int? port, Protocol correspondingProtocol)
        {
            return port ?? correspondingProtocol.DefaultPort;
        }

        private static void ValidateProtocol(Protocol? correspondingProtocol)
        {
            if (correspondingProtocol == null)
            {
                throw new FormatException(ExceptionMessages.InvalidProtocol);
            }
        }

        private void ValidatePort(int? port)
        {
            if (port == null)
            {
                return;
            }

            if (port.Value < 1 || port.Value > 65535)
            {
                throw new FormatException(ExceptionMessages.PortOutOfRange);
            }
        }

        private static void ValidateSubdomain(string subdomain)
        {
            // empty string is allowed
            if (!string.IsNullOrEmpty(subdomain) && (!char.IsLetter(subdomain.First()) || !subdomain.All(x => char.IsLetterOrDigit(x))))
            {
                throw new FormatException(ExceptionMessages.InvalidSubdomain);
            }
        }

        private void ValidateQuery(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return;
            }

            if (!query.All(x => char.IsLetterOrDigit(x) || x == '&'))
            {
                throw new FormatException(ExceptionMessages.InvalidQuery);
            }
        }

        private void ValidateAnchor(string anchor)
        {
            if (string.IsNullOrEmpty(anchor))
            {
                return;
            }

            if (!anchor.All(x => char.IsLetterOrDigit(x)))
            {
                throw new FormatException(ExceptionMessages.InvalidAnchor);
            }
        }

        private void ValidatePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            if (!path.All(x => char.IsLetterOrDigit(x) || x == '.' || x == '/' || x == '?' || x == '&' || x == '%'))
            {
                throw new FormatException(ExceptionMessages.InvalidPath);
            }
        }

        private static void ValidateDomain(string domain)
        {
            var splittedDomain = domain.Split('.');

            if (splittedDomain.Length == 1 && splittedDomain[0] == "localhost")
            {
                return;
            }

            if (!_topLevelDomains.Contains(splittedDomain[1]))
            {
                throw new FormatException(ExceptionMessages.TopLevelDomainNotSupported);
            }

            if (!splittedDomain[0].All(x => char.IsLetterOrDigit(x)))
            {
                throw new FormatException(ExceptionMessages.InvalidDomain);
            }
        }

        public string Protocol { get; }
        public string Subdomain { get; }
        public string Domain { get; }
        public int Port { get; private set; }
        public string Path { get; }
        public string Query { get; }
        public IEnumerable<char> Anchor { get; set; }
    }
}