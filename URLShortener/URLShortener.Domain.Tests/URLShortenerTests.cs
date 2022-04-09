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
        [InlineData("4141")]
        [InlineData("http://")]
        public void GetShortUrl_ShouldThrowUriFormatException_IfLongUrlIsNotValidUrl(string url)
        {
            Assert.Throws<UriFormatException>(() => _urlShortener.GetShortUrl(url));
        }
    }
}