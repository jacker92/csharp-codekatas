using BalancedBrackets.Domain.Models;
using Moq;
using System;
using Xunit;

namespace BalancedBrackets.Domain.Tests
{
    public class BracketParserTests
    {
        private readonly BracketParser _bracketParser;
        private Mock<IBracketResultConverter> _bracketResultConverter;

        public BracketParserTests()
        {
            _bracketResultConverter = new Mock<IBracketResultConverter>();
            _bracketParser = new BracketParser(_bracketResultConverter.Object);
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullString()
        {
            Assert.Throws<ArgumentNullException>(() => _bracketParser.Parse(null));
        }

        [Fact]
        public void ShouldReturnEmptyString_WhenPassedEmptyString()
        {
            var result = _bracketParser.Parse(string.Empty);
            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [InlineData("[]")]
        [InlineData("[][]")]
        [InlineData("[[]]")]
        [InlineData("[[[][]]]")]
        public void ShouldReturnOk_WhenPassedCorrectString(string input)
        {
            _bracketResultConverter.Setup(x => x.ConvertBracketParsingResult(BracketParsingResult.Ok))
                .Returns("OK");

            var result = _bracketParser.Parse(input);
            Assert.Equal("OK", result);
        }

        [Theory]
        [InlineData("][")]
        [InlineData("][][")]
        [InlineData("[][]][")]
        public void ShouldReturnFail_WhenPassedInvalidString(string input)
        {
            _bracketResultConverter.Setup(x => x.ConvertBracketParsingResult(BracketParsingResult.Fail))
             .Returns("FAIL");

            var result = _bracketParser.Parse(input);
            Assert.Equal("FAIL", result);
        }
    }
}
