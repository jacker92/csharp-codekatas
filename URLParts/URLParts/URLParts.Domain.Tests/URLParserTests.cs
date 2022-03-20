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

        [Fact]
        public void Decompose_ShouldThrowFormatException_WithURLInInvalidFormat()
        {
            var parser = new URLParser();
            Assert.Throws<FormatException>(() => parser.Decompose("asdf"));
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