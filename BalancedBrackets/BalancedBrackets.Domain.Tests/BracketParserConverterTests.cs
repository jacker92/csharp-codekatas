using BalancedBrackets.Domain.Models;
using Xunit;

namespace BalancedBrackets.Domain.Tests
{
    public class BracketResultConverterTests
    {
        [Theory]
        [InlineData(0, "OK")]
        [InlineData(1, "FAIL")]
        public void ConvertBracketParsingResult_ShouldProduceCorrectResult(int key, string convertedValue)
        {
            var bracketResultConverter = new BracketResultConverter();
            var result = bracketResultConverter.ConvertBracketParsingResult((BracketParsingResult)key);
            Assert.Equal(convertedValue, result);
        }
    }
}
