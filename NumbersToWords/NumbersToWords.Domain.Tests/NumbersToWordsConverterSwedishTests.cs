using NumbersToWords.Domain.Languages;
using NumbersToWords.Domain.Services;
using System;
using Xunit;

namespace NumbersToWords.Domain.Tests
{
    public class NumbersToWordsConverterSwedishTests
    {
        private readonly NumbersToWordsConverterService _numbersToWordsConverter;

        public NumbersToWordsConverterSwedishTests()
        {
            _numbersToWordsConverter = new NumbersToWordsConverterService();
        }

        [Fact]
        public void Convert_ShouldThrowArgumentOutOfRangeException_WithNegativeNumbers()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _numbersToWordsConverter.Convert(-1));
        }

        [Theory]
        [InlineData(0, "noll")]
        [InlineData(1, "ett")]
        [InlineData(2, "två")]
        [InlineData(3, "tre")]
        [InlineData(4, "fyra")]
        [InlineData(5, "fem")]
        [InlineData(6, "sex")]
        [InlineData(7, "sju")]
        [InlineData(8, "åtta")]
        [InlineData(9, "nio")]
        public void Convert_ShouldReturnCorrectResult_ForOneDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(10, "tio")]
        [InlineData(11, "elva")]
        [InlineData(12, "tolv")]
        [InlineData(13, "tretton")]
        [InlineData(14, "fjorton")]
        [InlineData(15, "femton")]
        [InlineData(16, "sexton")]
        [InlineData(17, "sjutton")]
        [InlineData(18, "arton")]
        [InlineData(19, "nitton")]
        [InlineData(20, "tjugo")]
        public void Convert_ShouldReturnCorrectResult_ForTwoDigitNumbers10To20(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(21, "tjugoett")]
        [InlineData(22, "tjugotvå")]
        [InlineData(23, "tjugotre")]
        [InlineData(24, "tjugofyra")]
        [InlineData(30, "trettio")]
        [InlineData(40, "fyrtio")]
        [InlineData(50, "femtio")]
        [InlineData(60, "sextio")]
        [InlineData(70, "sjuttio")]
        [InlineData(80, "åttio")]
        [InlineData(90, "nittio")]
        [InlineData(95, "nittiofem")]
        public void Convert_ShouldReturnCorrectResult_ForTwoDigitNumbers20To99(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(100, "etthundra")]
        [InlineData(200, "tvåhundra")]
        [InlineData(500, "femhundra")]
        public void Convert_ShouldReturnCorrectResult_ForEvenThreeDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(105, "etthundrafem")]
        [InlineData(539, "femhundratrettionio")]
        [InlineData(999, "niohundranittionio")]
        [InlineData(810, "åttahundratio")]
        public void Convert_ShouldReturnCorrectResult_ForThreeDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(1000, "ettusen")]
        [InlineData(2000, "tvåtusen")]
        [InlineData(5000, "femtusen")]
        public void Convert_ShouldReturnCorrectResult_ForEvenFourDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(1100, "ettusen etthundra")]
        [InlineData(5123, "femtusen etthundratjugotre")]
        [InlineData(9999, "niotusen niohundranittionio")]
        public void Convert_ShouldReturnCorrectResult_ForFourDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(10000, "tiotusen")]
        [InlineData(50000, "femtiotusen")]
        public void Convert_ShouldReturnCorrectResult_ForEvenFiveDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(10100, "tiotusen etthundra")]
        [InlineData(10001, "tiotusen ett")]
        [InlineData(99000, "nittioniotusen")]
        [InlineData(99999, "nittioniotusen niohundranittionio")]
        public void Convert_ShouldReturnCorrectResult_ForFiveDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(100000, "etthundratusen")]
        [InlineData(500000, "femhundratusen")]
        [InlineData(900000, "niohundratusen")]
        public void Convert_ShouldReturnCorrectResult_ForEvenSixDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(100001, "etthundratusen ett")]
        [InlineData(505050, "femhundrafemtusen femtio")]
        [InlineData(999999, "niohundranittioniotusen niohundranittionio")]
        public void Convert_ShouldReturnCorrectResult_ForSixDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(1000000, "en miljon")]
        [InlineData(5000000, "fem miljoner")]
        [InlineData(9000000, "nio miljoner")]
        public void Convert_ShouldReturnCorrectResult_ForEvenSevenDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(1000001, "en miljon ett")]
        [InlineData(5050505, "fem miljoner femtiotusen femhundrafem")]
        [InlineData(9999999, "nio miljoner niohundranittioniotusen niohundranittionio")]
        public void Convert_ShouldReturnCorrectResult_ForSevenDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(10000000, "tio miljoner")]
        [InlineData(50000000, "femtio miljoner")]
        [InlineData(90000000, "nittio miljoner")]
        public void Convert_ShouldReturnCorrectResult_ForEvenEightDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(10000100, "tio miljoner etthundra")]
        [InlineData(20001001, "tjugo miljoner ettusen ett")]
        [InlineData(55555555, "femtiofem miljoner femhundrafemtiofemtusen femhundrafemtiofem")]
        [InlineData(99999999, "nittionio miljoner niohundranittioniotusen niohundranittionio")]
        public void Convert_ShouldReturnCorrectResult_ForEightDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(100000000, "etthundra miljoner")]
        [InlineData(500000000, "femhundra miljoner")]
        [InlineData(900000000, "niohundra miljoner")]
        public void Convert_ShouldReturnCorrectResult_ForEvenNineDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Swedish);
            Assert.Equal(convertedValue, result);
        }


        //[Theory]
        //[InlineData(100000001, "one hundred million one")]
        //[InlineData(200010009, "two hundred million ten thousand nine")]
        //[InlineData(555555555, "five hundred fifty-five million five hundred fifty-five thousand five hundred fifty-five")]
        //[InlineData(999999999, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine")]
        //public void Convert_ShouldReturnCorrectResult_ForNineDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}
    }
}
