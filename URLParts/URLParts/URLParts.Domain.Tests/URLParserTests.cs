using System;
using Xunit;

namespace URLParts.Domain.Tests
{
    public class URLParserTests
    {
        [Fact]
        public void Decompose_ShouldThrowArgumentException_WithEmptyUrl()
        {
            var parser = new URLParser();
            Assert.Throws<ArgumentException>(() => parser.Decompose(string.Empty));
        }

        [Theory]
        [InlineData("asdf")]
        [InlineData("http")]
        public void Decompose_ShouldThrowFormatException_WithURLInInvalidFormat(string url)
        {
            var parser = new URLParser();
            Assert.Throws<FormatException>(() => parser.Decompose(url));
        }

        [Fact]
        public void Decompose_ShouldWorkCorrecly()
        {
            var parser = new URLParser();
            Url url = parser.Decompose("http://google.fi");

            Assert.Equal("http", url.Protocol);
        }
    }
}