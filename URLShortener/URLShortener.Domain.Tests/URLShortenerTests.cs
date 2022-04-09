using System;
using Xunit;

namespace URLShortener.Domain.Tests
{
    public class URLShortenerTests
    {
        [Fact]
        public void GetShortUrl_ShouldThrowArgumentNullException_IfLongUrlIsNull()
        {
            var urlShortener = new URLShortener();
            Assert.Throws<ArgumentNullException>(() => urlShortener.GetShortUrl(null));
        }
    }
}