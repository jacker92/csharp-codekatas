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

            var domains = GetDomains(domainAndRest);

            var domain = domainAndRest;
            var subdomain = string.Empty;
            if (domains.Count() > 2)
            {
                subdomain = domains[0];
                domain = domainAndRest.Split('.', 2)[1];
            }

            var domainWithPossiblePortSplitted = domain.Split(':');

            if (HasExplicitPortDefined(domainWithPossiblePortSplitted))
            {
                return new Url(protocol, subdomain, domainWithPossiblePortSplitted[0]);
            }

            if (!PortIsInCorrectFormat(domainWithPossiblePortSplitted, out int port))
            {
                throw new FormatException();
            }

            return new Url(protocol, subdomain, domainWithPossiblePortSplitted[0], port);
        }

        private static bool PortIsInCorrectFormat(string[] domainWithPossiblePortSplitted, out int port)
        {
            return int.TryParse(domainWithPossiblePortSplitted[1], out port);
        }

        private static bool HasExplicitPortDefined(string[] domainWithPossiblePortSplitted)
        {
            return domainWithPossiblePortSplitted.Length == 1;
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