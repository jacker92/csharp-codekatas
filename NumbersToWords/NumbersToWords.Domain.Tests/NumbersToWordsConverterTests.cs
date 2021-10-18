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
        [InlineData(1, "one")]
        [InlineData(2, "two")]
        [InlineData(3, "three")]
        [InlineData(4, "four")]
        [InlineData(5, "five")]
        [InlineData(6, "six")]
        [InlineData(7, "seven")]
        [InlineData(8, "eight")]
        [InlineData(9, "nine")]
        public void Convert_ShouldReturnCorrectResult_ForOneDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(10, "ten")]
        [InlineData(11, "eleven")]
        [InlineData(12, "twelve")]
        [InlineData(13, "thirteen")]
        [InlineData(14, "fourteen")]
        [InlineData(15, "fifteen")]
        [InlineData(16, "sixteen")]
        [InlineData(17, "seventeen")]
        [InlineData(18, "eighteen")]
        [InlineData(19, "nineteen")]
        [InlineData(20, "twenty")]
        public void Convert_ShouldReturnCorrectResult_ForTwoDigitNumbers10To20(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(21, "twenty-one")]
        [InlineData(22, "twenty-two")]
        [InlineData(23, "twenty-three")]
        [InlineData(24, "twenty-four")]
        [InlineData(30, "thirty")]
        [InlineData(40, "fourty")]
        [InlineData(50, "fifty")]
        [InlineData(60, "sixty")]
        [InlineData(70, "seventy")]
        [InlineData(80, "eighty")]
        [InlineData(90, "ninety")]
        [InlineData(95, "ninety-five")]
        public void Convert_ShouldReturnCorrectResult_ForTwoDigitNumbers20To99(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(100, "one hundred")]
        [InlineData(200, "two hundred")]
        [InlineData(500, "five hundred")]
        public void Convert_ShouldReturnCorrectResult_ForEvenThreeDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(105, "one hundred five")]
        [InlineData(539, "five hundred thirty-nine")]
        [InlineData(999, "nine hundred ninety-nine")]
        [InlineData(810, "eight hundred ten")]
        public void Convert_ShouldReturnCorrectResult_ForThreeDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value);
            Assert.Equal(convertedValue, result);
        }
    }
}
