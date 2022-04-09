using FluentAssertions;
using System;
using Xunit;

namespace URLShortener.Domain.Tests
{
    public class URLShortenerTests
    {
        private readonly URLShortener _urlShortener;

        public URLShortenerTests()
        {
            _urlShortener = new URLShortener();
        }

        [Fact]
        public void GetShortUrl_ShouldThrowArgumentNullException_IfLongUrlIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _urlShortener.GetShortUrl(null));
        }

        [Theory]
        [InlineData("asdf")]
        public void GetShortUrl_ShouldThrowUriFormatException_IfLongUrlIsNotValidUrl(string url)
        {
            var exception = Assert.Throws<UriFormatException>(() => _urlShortener.GetShortUrl(url));
            exception.Message.Should().BeEquivalentTo("Invalid URI: The format of the URI could not be determined.");
        }
    }
}