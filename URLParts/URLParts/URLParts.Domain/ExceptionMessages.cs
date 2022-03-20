using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLParts.Domain
{
    public static class ExceptionMessages
    {
        public const string InvalidUrl = "The URL is invalid.";
        public const string InvalidProtocol = "The URL protocol is invalid.";
        public const string InvalidDomain = "The URL domain is invalid.";
        public const string TopLevelDomainNotSupported = "The URL top domain is not supported";
        public const string InvalidSubdomain = "The URL subdomain is invalid.";
        public const string PortOutOfRange = "The URL port is out of range. The allowed range is from 1 to 65535.";
        public const string InvalidPort = "The URL port is invalid.";
        public const string InvalidAnchor = "The URL anchor is invalid.";
        public const string InvalidQuery = "The URL query parameters are invalid.";
        public const string InvalidPath = "The URL path is invalid.";
    }
}
