using System;
using Xunit;

namespace URLParts.Domain.Tests
{
    public class URLParserTests
    {
        private readonly URLParser _urlParser;

        public URLParserTests()
        {
            _urlParser = new URLParser();
        }

        [Fact]
        public void Decompose_ShouldThrowArgumentException_WithEmptyUrl()
        {
            Assert.Throws<ArgumentException>(() => _urlParser.Decompose(string.Empty));
        }

        [Theory]
        [InlineData("asdf")]
        [InlineData("http")]
        [InlineData("http:")]
        [InlineData("http://")]
        [InlineData("http://.")]
        [InlineData("http://a.c")]
        [InlineData("http://&.fi")]
        [InlineData("http://1.1.fi")]
        [InlineData("http://..fi")]
        public void Decompose_ShouldThrowFormatException_WithURLInInvalidFormat(string url)
        {
            Assert.Throws<FormatException>(() => _urlParser.Decompose(url));
        }

        [Theory]
        [InlineData("http://foo.google.fi", "http", "foo", "google.fi", 80)]
        [InlineData("http://1.fi", "http", "", "1.fi", 80)]
        [InlineData("http://a.1.fi", "http", "a", "1.fi", 80)]
        [InlineData("http://foo.google.net", "http", "foo", "google.net", 80)]
        [InlineData("http://foo.google.org", "http", "foo", "google.org", 80)]
        [InlineData("http://foo.google.int", "http", "foo", "google.int", 80)]
        [InlineData("http://foo.google.edu", "http", "foo", "google.edu", 80)]
        [InlineData("http://foo.google.gov", "http", "foo", "google.gov", 80)]
        [InlineData("http://foo.google.mil", "http", "foo", "google.mil", 80)]
        [InlineData("http://foo.google.com", "http", "foo", "google.com", 80)]
        [InlineData("https://google.fi", "https", "", "google.fi", 443)]
        [InlineData("ftp://google.fi", "ftp", "", "google.fi", 21)]
        [InlineData("sftp://google.fi", "sftp", "", "google.fi", 22)]
        public void Decompose_ShouldWorkCorrecly(string url, string expectedProtocol, string expectedSubdomain, string expectedDomain, int expectedPort)
        {
            var result = _urlParser.Decompose(url);

            Assert.Equal(expectedProtocol, result.Protocol);
            Assert.Equal(expectedSubdomain, result.Subdomain);
            Assert.Equal(expectedDomain, result.Domain);
            Assert.Equal(expectedPort, result.Port);
        }
    }
}