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
    }
}