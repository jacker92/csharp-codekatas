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

            ValidateDomain(domain);

            Domain = domain;

            ValidatePath(path);

            Path = path;

            Query = query;

            ValidateAnchor(anchor);

            Anchor = anchor;
        }

        private void ValidateAnchor(string anchor)
        {
            if (string.IsNullOrEmpty(anchor))
            {
                return;
            }

            if (!anchor.All(x => char.IsLetterOrDigit(x)))
            {
                throw new FormatException();
            }
        }

        private void ValidatePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            if (!path.All(x => char.IsLetterOrDigit(x) || x == '.' || x == '/'))
            {
                throw new FormatException();
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
                throw new FormatException();
            }

            if (!splittedDomain[0].All(x => char.IsLetterOrDigit(x)))
            {
                throw new FormatException();
            }
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
        public int Port { get; private set; }
        public string Path { get; }
        public string Query { get; }
        public IEnumerable<char> Anchor { get; set; }
    }
}