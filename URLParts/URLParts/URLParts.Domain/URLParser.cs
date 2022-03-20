﻿namespace URLParts.Domain
{
    public class Url
    {
        public string Protocol { get; set; }
        public string Subdomain { get; set; }
    }

    public class URLParser
    {
        private static readonly List<string> _protocols = new List<string> { "http", "https", "ftp", "sftp" };

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

            var domainAndRest = url.Split("//")[1];
            var domains = domainAndRest.Split('.');

            if (domains.Any(domain => string.IsNullOrWhiteSpace(domain)))
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

            return new Url
            {
                Protocol = protocol,
            };
        }
    }
}