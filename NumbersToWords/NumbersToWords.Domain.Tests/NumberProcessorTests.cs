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
        [InlineData(1, 1)]
        public void GetLastDigit_ShouldReturnCorrectResult(int input, int expected)
        {
            var result = _numberProcessor.GetLastDigit(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(15, 1)]
        [InlineData(155, 1)]
        [InlineData(10, 1)]
        [InlineData(70, 7)]
        [InlineData(1, 1)]
        public void GetFirstDigit_ShouldReturnCorrectResult(int input, int expected)
        {
            var result = _numberProcessor.GetFirstDigit(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(65, 65)]
        [InlineData(155, 55)]
        [InlineData(1575, 75)]
        [InlineData(1, 1)]
        public void GetTwoDigitNumber_ShouldReturnCorrectResult(int input, int expected)
        {
            var result = _numberProcessor.GetTwoDigitNumber(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(65, 60)]
        [InlineData(155, 50)]
        [InlineData(1575, 70)]
        [InlineData(1, 0)]
        public void GetEvenTwoDigitNumber_ShouldReturnCorrectResult(int input, int expected)
        {
            var result = _numberProcessor.GetEvenTwoDigitNumber(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(655, 600)]
        [InlineData(155, 100)]
        [InlineData(1575, 500)]
        [InlineData(1, 0)]
        [InlineData(12, 0)]
        public void GetEvenThreeDigitNumber_ShouldReturnCorrectResult(int input, int expected)
        {
            var result = _numberProcessor.GetEvenThreeDigitNumber(input);
            Assert.Equal(expected, result);
        }
    }
}
