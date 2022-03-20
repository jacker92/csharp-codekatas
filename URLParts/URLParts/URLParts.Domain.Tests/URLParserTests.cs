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
        [InlineData("asdf", ExceptionMessages.InvalidUrl)]
        [InlineData("http", ExceptionMessages.InvalidUrl)]
        [InlineData("http:", ExceptionMessages.InvalidUrl)]
        [InlineData("http://", ExceptionMessages.InvalidUrl)]
        [InlineData("http://.", ExceptionMessages.InvalidDomain)]
        [InlineData("://google.fi", ExceptionMessages.InvalidProtocol)]
        [InlineData("http://a.c", ExceptionMessages.TopLevelDomainNotSupported)]
        [InlineData("http://a.info", ExceptionMessages.TopLevelDomainNotSupported)]
        [InlineData("http://&.fi", ExceptionMessages.InvalidDomain)]
        [InlineData("http://1.1.fi", ExceptionMessages.InvalidSubdomain)]
        [InlineData("http://..fi", ExceptionMessages.InvalidDomain)]
        [InlineData("http://www.google.fi:65536", ExceptionMessages.PortOutOfRange)]
        [InlineData("http://www.google.fi:0", ExceptionMessages.PortOutOfRange)]
        [InlineData("http://www.google.fi:asd", ExceptionMessages.InvalidPort)]
        [InlineData("http://www.google.fi:123/¤", ExceptionMessages.InvalidPath)]
        [InlineData("http://www.google.fi:123/asdf#&", ExceptionMessages.InvalidAnchor)]
        [InlineData("http://www.google.fi:123/asdf?%", ExceptionMessages.InvalidQuery)]
        [InlineData("http://www.google.fi:123/asdf?asdf&[", ExceptionMessages.InvalidQuery)]
        [InlineData("http://www.google.fi:123/asdf?asdf&sf#[", ExceptionMessages.InvalidAnchor)]
        public void Decompose_ShouldThrowFormatException_WithURLInInvalidFormat(string url, string expectedErrorMessage)
        {
            var error = Assert.Throws<FormatException>(() => _urlParser.Decompose(url));
            Assert.Equal(expectedErrorMessage, error.Message);
        }

        [Theory]
        [InlineData("http://foo.google.fi", "http", "foo", "google.fi", 80, "", "", "")]
        [InlineData("http://1.fi", "http", "", "1.fi", 80, "", "", "")]
        [InlineData("http://a.1.fi", "http", "a", "1.fi", 80, "", "", "")]
        [InlineData("http://foo.google.net", "http", "foo", "google.net", 80, "", "", "")]
        [InlineData("http://foo.google.org:81", "http", "foo", "google.org", 81, "", "", "")]
        [InlineData("http://foo.google.org:65535", "http", "foo", "google.org", 65535, "", "", "")]
        [InlineData("http://foo.google.org:1", "http", "foo", "google.org", 1, "", "", "")]
        [InlineData("http://foo.google.int", "http", "foo", "google.int", 80, "", "", "")]
        [InlineData("http://foo.google.edu", "http", "foo", "google.edu", 80, "", "", "")]
        [InlineData("http://foo.google.gov", "http", "foo", "google.gov", 80, "", "", "")]
        [InlineData("http://foo.google.mil", "http", "foo", "google.mil", 80, "", "", "")]
        [InlineData("http://foo.google.com", "http", "foo", "google.com", 80, "", "", "")]
        [InlineData("https://google.fi", "https", "", "google.fi", 443, "", "", "")]
        [InlineData("ftp://google.fi", "ftp", "", "google.fi", 21, "", "", "")]
        [InlineData("sftp://google.fi", "sftp", "", "google.fi", 22, "", "", "")]
        [InlineData("sftp://google.fi/a", "sftp", "", "google.fi", 22, "a", "", "")]
        [InlineData("http://foo.bar.com/foobar.html", "http", "foo", "bar.com", 80, "foobar.html", "", "")]
        [InlineData("ftp://foo.com:9000/files", "ftp", "", "foo.com", 9000, "files", "", "")]
        [InlineData("https://www.foobar.com:8080/download/install.exe", "https", "www", "foobar.com", 8080, "download/install.exe", "", "")]
        [InlineData("https://localhost/index.html", "https", "", "localhost", 443, "index.html", "", "")]
        [InlineData("https://localhost/index.html?hi", "https", "", "localhost", 443, "index.html", "hi", "")]
        [InlineData("https://localhost/index.html#footer", "https", "", "localhost", 443, "index.html", "", "footer")]
        [InlineData("https://localhost/index.html?hi#footer", "https", "", "localhost", 443, "index.html", "hi", "footer")]
        [InlineData("https://localhost/index.html?hi&world#footer", "https", "", "localhost", 443, "index.html", "hi&world", "footer")]
        [InlineData("https://localhost/katas/URL%20Parts.pdf", "https", "", "localhost", 443, "katas/URL%20Parts.pdf", "", "")]
        public void Decompose_ShouldWorkCorrecly(string url, string expectedProtocol, string expectedSubdomain, string expectedDomain, int expectedPort, string expectedPath, string expectedQuery, string expectedAnchor)
        {
            var result = _urlParser.Decompose(url);

            Assert.Equal(expectedProtocol, result.Protocol);
            Assert.Equal(expectedSubdomain, result.Subdomain);
            Assert.Equal(expectedDomain, result.Domain);
            Assert.Equal(expectedPort, result.Port);
            Assert.Equal(expectedPath, result.Path);
            Assert.Equal(expectedQuery, result.Query);
            Assert.Equal(expectedAnchor, result.Anchor);
        }
    }
}