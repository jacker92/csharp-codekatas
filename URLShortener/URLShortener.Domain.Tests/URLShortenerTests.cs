using FluentAssertions;
using System;
using System.Linq;
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

        [Fact]
        public void GetShortUrl_ShouldReturnShortenedUrl()
        {
            var result = _urlShortener.GetShortUrl("https://www.google.fi");
            result.Should().StartWith("https://short.url/");
            result.Split("/").Last().Length.Should().Be(7, "identifiable part should be 7 characters long");
        }
    }
}