namespace URLParts.Domain
{
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

            var protocol = GetProtocolName(url);

            var domainAndRest = url.Split("//")[1];

            var domainAndPath = domainAndRest.Split("/", 2);
            domainAndRest = domainAndPath[0];

            var domains = GetDomains(domainAndRest);

            var subdomain = GetSubdomain(domains);
            var domain = GetPrimaryDomain(domainAndRest, domains);

            var domainWithPossiblePortSplitted = domain.Split(':');

            var port = GetPort(domainWithPossiblePortSplitted);

            var pathWithParametersAndAnchor = GetPathWithParametersAndAnchor(domainAndPath);

            var fullPathSplitted = pathWithParametersAndAnchor.Split("#");
            var pathWithoutAnchor = fullPathSplitted[0];

            var query = GetQuery(pathWithoutAnchor);

            var pathWithoutQuery = pathWithoutAnchor.Split('?')[0];

            var anchor = GetAnchor(fullPathSplitted);

            return new Url(protocol, subdomain, domainWithPossiblePortSplitted[0], port, pathWithoutQuery, query, anchor);
        }

        private string GetAnchor(string[] fullPathSplitted)
        {
            if (fullPathSplitted.Length == 1)
            {
                return string.Empty;
            }

            return fullPathSplitted[1];
        }

        private string GetQuery(string pathWithoutAnchor)
        {
            var splittedPath = pathWithoutAnchor.Split('?');
            if (splittedPath.Length == 1)
            {
                return string.Empty;
            }

            return splittedPath[1];
        }

        private string GetPathWithParametersAndAnchor(string[] domainAndPath)
        {
            return domainAndPath.Length == 2 ? domainAndPath[1] : string.Empty;
        }

        private int? GetPort(string[] domainWithPossiblePortSplitted)
        {
            if (!HasExplicitPortDefined(domainWithPossiblePortSplitted))
            {
                return null;
            }

            var possiblePort = domainWithPossiblePortSplitted[1];

            if (!PortIsInCorrectFormat(possiblePort, out int port))
            {
                throw new FormatException();
            }

            return port;
        }

        private static string GetPrimaryDomain(string domainAndRest, string[] domains)
        {
            if (domains.Count() > 2)
            {
                return domainAndRest.Split('.', 2)[1];
            }

            return domainAndRest;
        }

        private static string GetSubdomain(string[] domains)
        {
            return domains.Count() > 2 ? domains[0] : string.Empty;
        }

        private static bool PortIsInCorrectFormat(string possiblePort, out int port)
        {
            return int.TryParse(possiblePort, out port);
        }

        private static bool HasExplicitPortDefined(string[] domainWithPossiblePortSplitted)
        {
            return domainWithPossiblePortSplitted.Length != 1;
        }

        private static string[] GetDomains(string domainAndRest)
        {
            var domains = domainAndRest.Split('.');

            if (domains.Any(domain => string.IsNullOrWhiteSpace(domain)))
            {
                throw new FormatException();
            }

            return domains;
        }

        private static string GetProtocolName(string url)
        {
            var protocol = url.Split(':').First();

            if (string.IsNullOrWhiteSpace(protocol))
            {
                throw new FormatException();
            }

            return protocol;
        }
    }
}