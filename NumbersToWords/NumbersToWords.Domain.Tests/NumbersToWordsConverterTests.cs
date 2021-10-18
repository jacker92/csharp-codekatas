using System;
using Xunit;

namespace NumbersToWords.Domain.Tests
{
    public class NumbersToWordsConverterTests
    {
        private readonly NumbersToWordsConverter _numbersToWordsConverter;

        public NumbersToWordsConverterTests()
        {
            _numbersToWordsConverter = new NumbersToWordsConverter();
        }

        [Fact]
        public void Convert_ShouldThrowArgumentOutOfRangeException_WithNegativeNumbers()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _numbersToWordsConverter.Convert(-1));
        }

        [Theory]
        [InlineData(0, "zero")]
        public void Convert_ShouldReturnCorrectResult(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value);
            Assert.Equal(convertedValue, result);

        }
    }
}
