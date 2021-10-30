using System;
using Xunit;

namespace NumbersToWords.Domain.Tests
{
    public class NumbersToWordsConverterFinnishTests
    {
        private readonly NumbersToWordsConverter _numbersToWordsConverter;

        public NumbersToWordsConverterFinnishTests()
        {
            _numbersToWordsConverter = new NumbersToWordsConverter();
        }

        [Fact]
        public void Convert_ShouldThrowArgumentOutOfRangeException_WithNegativeNumbers()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _numbersToWordsConverter.Convert(-1));
        }

        [Theory]
        [InlineData(0, "nolla")]
        [InlineData(1, "yksi")]
        [InlineData(2, "kaksi")]
        [InlineData(3, "kolme")]
        [InlineData(4, "neljä")]
        [InlineData(5, "viisi")]
        [InlineData(6, "kuusi")]
        [InlineData(7, "seitsemän")]
        [InlineData(8, "kahdeksan")]
        [InlineData(9, "yhdeksän")]
        public void Convert_ShouldReturnCorrectResult_ForOneDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(10, "kymmenen")]
        [InlineData(11, "yksitoista")]
        [InlineData(12, "kaksitoista")]
        [InlineData(13, "kolmetoista")]
        [InlineData(14, "neljätoista")]
        [InlineData(15, "viisitoista")]
        [InlineData(16, "kuusitoista")]
        [InlineData(17, "seitsemäntoista")]
        [InlineData(18, "kahdeksantoista")]
        [InlineData(19, "yhdeksäntoista")]
        [InlineData(20, "kaksikymmentä")]
        public void Convert_ShouldReturnCorrectResult_ForTwoDigitNumbers10To20(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        //[Theory]
        //[InlineData(21, "twenty-one")]
        //[InlineData(22, "twenty-two")]
        //[InlineData(23, "twenty-three")]
        //[InlineData(24, "twenty-four")]
        //[InlineData(30, "thirty")]
        //[InlineData(40, "fourty")]
        //[InlineData(50, "fifty")]
        //[InlineData(60, "sixty")]
        //[InlineData(70, "seventy")]
        //[InlineData(80, "eighty")]
        //[InlineData(90, "ninety")]
        //[InlineData(95, "ninety-five")]
        //public void Convert_ShouldReturnCorrectResult_ForTwoDigitNumbers20To99(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}

        //[Theory]
        //[InlineData(100, "one hundred")]
        //[InlineData(200, "two hundred")]
        //[InlineData(500, "five hundred")]
        //public void Convert_ShouldReturnCorrectResult_ForEvenThreeDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}

        //[Theory]
        //[InlineData(105, "one hundred five")]
        //[InlineData(539, "five hundred thirty-nine")]
        //[InlineData(999, "nine hundred ninety-nine")]
        //[InlineData(810, "eight hundred ten")]
        //public void Convert_ShouldReturnCorrectResult_ForThreeDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}

        //[Theory]
        //[InlineData(1000, "one thousand")]
        //[InlineData(2000, "two thousand")]
        //[InlineData(5000, "five thousand")]
        //public void Convert_ShouldReturnCorrectResult_ForEvenFourDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}

        //[Theory]
        //[InlineData(1100, "one thousand one hundred")]
        //[InlineData(5123, "five thousand one hundred twenty-three")]
        //[InlineData(9999, "nine thousand nine hundred ninety-nine")]
        //public void Convert_ShouldReturnCorrectResult_ForFourDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}

        //[Theory]
        //[InlineData(10000, "ten thousand")]
        //[InlineData(50000, "fifty thousand")]
        //public void Convert_ShouldReturnCorrectResult_ForEvenFiveDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}

        //[Theory]
        //[InlineData(10100, "ten thousand one hundred")]
        //[InlineData(10001, "ten thousand one")]
        //[InlineData(99000, "ninety-nine thousand")]
        //[InlineData(99999, "ninety-nine thousand nine hundred ninety-nine")]
        //public void Convert_ShouldReturnCorrectResult_ForFiveDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}

        //[Theory]
        //[InlineData(100000, "one hundred thousand")]
        //[InlineData(500000, "five hundred thousand")]
        //[InlineData(900000, "nine hundred thousand")]
        //public void Convert_ShouldReturnCorrectResult_ForEvenSixDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}

        //[Theory]
        //[InlineData(100001, "one hundred thousand one")]
        //[InlineData(505050, "five hundred five thousand fifty")]
        //[InlineData(999999, "nine hundred ninety-nine thousand nine hundred ninety-nine")]
        //public void Convert_ShouldReturnCorrectResult_ForSixDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}

        //[Theory]
        //[InlineData(1000000, "one million")]
        //[InlineData(5000000, "five million")]
        //[InlineData(9000000, "nine million")]
        //public void Convert_ShouldReturnCorrectResult_ForEvenSevenDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}

        //[Theory]
        //[InlineData(1000001, "one million one")]
        //[InlineData(5050505, "five million fifty thousand five hundred five")]
        //[InlineData(9999999, "nine million nine hundred ninety-nine thousand nine hundred ninety-nine")]
        //public void Convert_ShouldReturnCorrectResult_ForSevenDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}

        //[Theory]
        //[InlineData(10000000, "ten million")]
        //[InlineData(50000000, "fifty million")]
        //[InlineData(90000000, "ninety million")]
        //public void Convert_ShouldReturnCorrectResult_ForEvenEightDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}

        //[Theory]
        //[InlineData(10000100, "ten million one hundred")]
        //[InlineData(20001001, "twenty million one thousand one")]
        //[InlineData(55555555, "fifty-five million five hundred fifty-five thousand five hundred fifty-five")]
        //[InlineData(99999999, "ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine")]
        //public void Convert_ShouldReturnCorrectResult_ForEightDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}

        //[Theory]
        //[InlineData(100000000, "one hundred million")]
        //[InlineData(500000000, "five hundred million")]
        //[InlineData(900000000, "nine hundred million")]
        //public void Convert_ShouldReturnCorrectResult_ForEvenNineDigitNumbers(int value, string convertedValue)
        //{
        //    var result = _numbersToWordsConverter.Convert(value);
        //    Assert.Equal(convertedValue, result);
        //}


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
