using FluentAssertions;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace URLShortener.Domain.Tests
{
    public class URLShortenerTests
    {
        private readonly URLShortener _urlShortener;
        private readonly Mock<IDateTimeProvider> _dateTimeProvider;
        private readonly URLStatisticsFactory _urlStatisticsFactory;
        private readonly ShortURLRepository _shortURLRepository;
        private readonly URLShortenerService _urlShortenerService;

        public URLShortenerTests()
        {
            _dateTimeProvider = new Mock<IDateTimeProvider>();
            _urlStatisticsFactory = new URLStatisticsFactory(_dateTimeProvider.Object);
            _shortURLRepository = new ShortURLRepository(_dateTimeProvider.Object, _urlStatisticsFactory);
            _urlShortenerService = new URLShortenerService(_shortURLRepository);
            _urlShortener = new URLShortener(_urlShortenerService);
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
        public void Translate_ShouldThrowShortenedUrlNotFoundException_ForShortUrlNotFound()
        {
            var shortUrl = "https://short.url/abcd123";
            var exception = Assert.Throws<ShortenedUrlNotFoundException>(() => _urlShortener.Translate(shortUrl));
            exception.Message.Should().Be($"No match found for short url: {shortUrl}");
        }

        [Fact]
        public void Translate_ShouldReturnSameShortUrl_ForShortUrl()
        {
            var shortUrl = _urlShortener.GetShortUrl("https://google.fi");
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

        [Fact]
        public void GetStatistics_ShouldReturnCorrectStatistics_ForLongUrl_WithAccessedOnce_ByGetShortUrl()
        {
            var shortUrl = _urlShortener.GetShortUrl("https://google.fi");
            var statistics = _urlShortener.GetStatistics("https://google.fi");

            statistics.LongUrl.Should().Be("https://google.fi");
            statistics.ShortUrl.Should().Be(shortUrl);
            statistics.TimesAccessed.Should().HaveCount(1);
        }

        [Fact]
        public void GetStatistics_ShouldReturnCorrectStatistics_ForLongUrl_WithAccessedOnce_ByTranslate()
        {
            var shortUrl = _urlShortener.Translate("https://google.fi");
            var statistics = _urlShortener.GetStatistics("https://google.fi");

            statistics.LongUrl.Should().Be("https://google.fi");
            statistics.ShortUrl.Should().Be(shortUrl);
            statistics.TimesAccessed.Should().HaveCount(1);
        }

        [Fact]
        public void GetStatistics_ShouldReturnCorrectStatistics_ForLongUrl_WithAccessedTwice_ByGetShortUrl()
        {
            _urlShortener.GetShortUrl("https://google.fi");
            var shortUrl = _urlShortener.GetShortUrl("https://google.fi");

            var statistics = _urlShortener.GetStatistics("https://google.fi");

            statistics.LongUrl.Should().Be("https://google.fi");
            statistics.ShortUrl.Should().Be(shortUrl);
            statistics.TimesAccessed.Should().HaveCount(2);
        }

        [Fact]
        public void GetStatistics_ShouldReturnCorrectStatistics_ForLongUrl_WithAccessedTwice_ByTranslate()
        {
            _urlShortener.Translate("https://google.fi");
            var shortUrl = _urlShortener.Translate("https://google.fi");

            var statistics = _urlShortener.GetStatistics("https://google.fi");

            statistics.LongUrl.Should().Be("https://google.fi");
            statistics.ShortUrl.Should().Be(shortUrl);
            statistics.TimesAccessed.Should().HaveCount(2);
        }

        [Fact]
        public void GetStatistics_ShouldReturnCorrectStatistics_ForShortUrl()
        {
            var date = DateTime.UtcNow;
            _dateTimeProvider.Setup(x => x.DateTimeNow).Returns(date);

            var shortUrl = _urlShortener.GetShortUrl("https://google.fi");
            var statistics = _urlShortener.GetStatistics(shortUrl);
            statistics.LongUrl.Should().Be("https://google.fi");
            statistics.ShortUrl.Should().Be(shortUrl);
            statistics.TimesAccessed.Should().HaveCount(1);
            statistics.TimesAccessed.First().Timestamp.Should().Be(date);
        }

        [Fact]
        public void Log_ShouldThrowArgumentNullException_WithNullUrl()
        {
            Assert.Throws<ArgumentNullException>(() => _urlShortener.Log(null));
        }

        [Theory]
        [InlineData("asdf")]
        [InlineData("4141")]
        [InlineData("http://")]
        public void Log_ShouldThrowUriFormatException_IfLongUrlIsNotValidUrl(string url)
        {
            Assert.Throws<UriFormatException>(() => _urlShortener.Log(url));
        }

        [Fact]
        public void Log_ShouldGenerateCorrectLogOutput()
        {
            var date = DateTime.UtcNow;
            _dateTimeProvider.Setup(x => x.DateTimeNow).Returns(date);

            var shortUrl = _urlShortener.GetShortUrl("https://google.fi");
            var statistics = _urlShortener.GetStatistics(shortUrl);
            var log = _urlShortener.Log(shortUrl);
            string expected = GetExpectedLog(statistics);
            log.Should().Be(expected);
        }

        private static string GetExpectedLog(UrlStatistics statistics)
        {
            var expected = $"#Log info for url: {statistics.LongUrl}({statistics.ShortUrl})#\n";
            expected += $"Number of accesses: {statistics.TimesAccessed.Count}\n";
            expected += $"Access #1: {statistics.TimesAccessed.First().Timestamp:G}\n";
            return expected;
        }
    }
}