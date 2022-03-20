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
        public void Decompose_ShouldThrowFormatException_WithURLInInvalidFormat(string url)
        {
            Assert.Throws<FormatException>(() => _urlParser.Decompose(url));
        }

        [Theory]
        [InlineData("http://foo.google.fi", "http", "foo", "google.fi")]
        [InlineData("https://google.fi", "https", "", "google.fi")]
        [InlineData("ftp://google.fi", "ftp", "", "google.fi")]
        [InlineData("sftp://google.fi", "sftp", "", "google.fi")]
        public void Decompose_ShouldWorkCorrecly(string url, string expectedProtocol, string expectedSubdomain, string expectedDomain)
        {
            var result = _urlParser.Decompose(url);

            Assert.Equal(expectedProtocol, result.Protocol);
            Assert.Equal(expectedDomain, result.Domain);
        }
    }
}