using System;
using Xunit;

namespace EndOfLineWhitespaceTrimming.Domain.Tests
{
    public class WhitespaceTrimmerTests
    {
        private readonly WhitespaceTrimmer _whitespaceTrimmer;

        public WhitespaceTrimmerTests()
        {
            _whitespaceTrimmer = new WhitespaceTrimmer();
        }

        [Fact]
        public void Trim_ShouldThrowArgumentNullException_WithNullString()
        {
            Assert.Throws<ArgumentNullException>(() => _whitespaceTrimmer.Trim(null));
        }

        [Theory]
        [InlineData("asdf", "asdf")]
        [InlineData("asdf ", "asdf")]
        [InlineData("asdf\t", "asdf")]
        [InlineData(" asdf", " asdf")]
        [InlineData("ab\r\n cd \r\n", "ab\r\n cd\r\n")]
        [InlineData("\r\n", "\r\n")]
        public void Trim_ShouldReturnCorrectResult(string stringToTrim, string expected)
        {
            var result = _whitespaceTrimmer.Trim(stringToTrim);    

            Assert.Equal(expected, result);
        }
    }
}