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
        public void Decompose_ShouldThrowFormatException_WithURLInInvalidFormat(string url)
        {
            Assert.Throws<FormatException>(() => _urlParser.Decompose(url));
        }

        [Theory]
        [InlineData("http://google.fi", "http")]
        [InlineData("https://google.fi", "https")]
        [InlineData("ftp://google.fi", "ftp")]
        [InlineData("sftp://google.fi", "sftp")]
        public void Decompose_ShouldWorkCorrecly(string url, string expectedProtocol)
        {
            var result = _urlParser.Decompose(url);

            Assert.Equal(expectedProtocol, result.Protocol);
        }
    }
}