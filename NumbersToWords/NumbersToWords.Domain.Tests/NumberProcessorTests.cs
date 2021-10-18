using Xunit;

namespace NumbersToWords.Domain.Tests
{
    public class NumberProcessorTests
    {
        private readonly NumberProcessor _numberProcessor;

        public NumberProcessorTests()
        {
            _numberProcessor = new NumberProcessor();
        }

        [Theory]
        [InlineData(15, 5)]
        [InlineData(155, 5)]
        [InlineData(10, 0)]
        public void GetOneDigitNumber_ShouldReturnCorrectResult(int input, int expected)
        {
            var result = _numberProcessor.GetOneDigitNumber(input);
            Assert.Equal(expected, result);
        }
    }
}
