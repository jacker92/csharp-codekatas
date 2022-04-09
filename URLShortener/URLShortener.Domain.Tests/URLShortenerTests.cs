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

        [Fact]
        public void GetShortUrl_ShouldReturnDifferentShortenedUrlsForTwoDifferentUrls()
        {
            var result = _urlShortener.GetShortUrl("https://www.google.fi");
            var result2 = _urlShortener.GetShortUrl("https://www.yahoo.fi");

            result.Should().NotBe(result2);
        }

        [Fact]
        public void GetShortUrl_ShouldReturnSameShortenedUrl_ForSameUrl()
        {
            var result = _urlShortener.GetShortUrl("https://www.google.fi");
            var result2 = _urlShortener.GetShortUrl("https://www.google.fi");

            result.Should().Be(result2);
        }

        [Fact]
        public void Translate_ShouldThrowArgumentNullException_IfUrlIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _urlShortener.Translate(null));
        }

        [Theory]
        [InlineData("asdf")]
        [InlineData("4141")]
        [InlineData("http://")]
        public void Translate_ShouldThrowUriFormatException_IfUrlIsNotValidUrl(string url)
        {
            Assert.Throws<UriFormatException>(() => _urlShortener.Translate(url));
        }

        [Fact]
        public void Translate_ShouldReturnShortenedUrl_ForLongUrl()
        {
            var result = _urlShortener.Translate("https://www.google.fi");
            result.Should().StartWith("https://short.url/");
            result.Split("/").Last().Length.Should().Be(7, "identifiable part should be 7 characters long");
        }

        [Fact]
        public void Translate_ShouldReturnSameShortUrl_ForShortUrl()
        {
            var shortUrl = "https://short.url/abcd123";
            var result = _urlShortener.Translate(shortUrl);
            result.Should().Be(shortUrl);
        }

        [Fact]
        public void Translate_ShouldReturnSameShortenedUrl_ForSameUrl()
        {
            var result = _urlShortener.Translate("https://www.google.fi");
            var result2 = _urlShortener.Translate("https://www.google.fi");
            var result3 = _urlShortener.Translate(result);

            result.Should().Be(result2).And.Be(result3);
        }


        [Fact]
        public void GetStatistics_ShouldThrowArgumentNullException_IfUrlIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _urlShortener.GetStatistics(null));
        }

        [Theory]
        [InlineData("asdf")]
        [InlineData("4141")]
        [InlineData("http://")]
        public void GetStatistics_ShouldThrowUriFormatException_IfUrlIsNotValidUrl(string url)
        {
            Assert.Throws<UriFormatException>(() => _urlShortener.GetStatistics(url));
        }

        [Fact]
        public void GetStatistics_ShouldThrowShortenedUrlNotFoundException_IfUrlWasNotFound()
        {
            var exception = Assert.Throws<ShortenedUrlNotFoundException>(() => _urlShortener.GetStatistics("https://google.fi"));
            exception.Message.Should().Be("No statistics found for url: https://google.fi");
        }

        //[Fact]
        //public void GetStatistics_ShouldReturnCorrectStatistics_ForShortenedUrl()
        //{
        //    var result = _urlShortener.GetShortUrl("https://google.fi");
        //    var statistics = _urlShortener.GetStatistics(result);
        //}
    }
}