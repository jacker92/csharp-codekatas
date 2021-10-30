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

        [Theory]
        [InlineData(21, "kaksikymmentäyksi")]
        [InlineData(22, "kaksikymmentäkaksi")]
        [InlineData(23, "kaksikymmentäkolme")]
        [InlineData(24, "kaksikymmentäneljä")]
        [InlineData(30, "kolmekymmentä")]
        [InlineData(40, "neljäkymmentä")]
        [InlineData(50, "viisikymmentä")]
        [InlineData(60, "kuusikymmentä")]
        [InlineData(70, "seitsemänkymmentä")]
        [InlineData(80, "kahdeksankymmentä")]
        [InlineData(90, "yhdeksänkymmentä")]
        [InlineData(95, "yhdeksänkymmentäviisi")]
        public void Convert_ShouldReturnCorrectResult_ForTwoDigitNumbers20To99(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(100, "sata")]
        [InlineData(200, "kaksisataa")]
        [InlineData(500, "viisisataa")]
        public void Convert_ShouldReturnCorrectResult_ForEvenThreeDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(105, "sataviisi")]
        [InlineData(539, "viisisataakolmekymmentäyhdeksän")]
        [InlineData(999, "yhdeksänsataayhdeksänkymmentäyhdeksän")]
        [InlineData(810, "kahdeksansataakymmenen")]
        public void Convert_ShouldReturnCorrectResult_ForThreeDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(1000, "tuhat")]
        [InlineData(2000, "kaksituhatta")]
        [InlineData(5000, "viisituhatta")]
        public void Convert_ShouldReturnCorrectResult_ForEvenFourDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(1100, "tuhatsata")]
        [InlineData(5123, "viisituhattasatakaksikymmentäkolme")]
        [InlineData(9999, "yhdeksäntuhattayhdeksänsataayhdeksänkymmentäyhdeksän")]
        public void Convert_ShouldReturnCorrectResult_ForFourDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(10000, "kymmenentuhatta")]
        [InlineData(50000, "viisikymmentätuhatta")]
        public void Convert_ShouldReturnCorrectResult_ForEvenFiveDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(10100, "kymmenentuhattasata")]
        [InlineData(10001, "kymmenentuhattayksi")]
        [InlineData(99000, "yhdeksänkymmentäyhdeksäntuhatta")]
        [InlineData(99999, "yhdeksänkymmentäyhdeksäntuhattayhdeksänsataayhdeksänkymmentäyhdeksän")]
        public void Convert_ShouldReturnCorrectResult_ForFiveDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(100000, "satatuhatta")]
        [InlineData(500000, "viisisataatuhatta")]
        [InlineData(900000, "yhdeksänsataatuhatta")]
        public void Convert_ShouldReturnCorrectResult_ForEvenSixDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(100001, "satatuhattayksi")]
        [InlineData(505050, "viisisataaviisituhattaviisikymmentä")]
        [InlineData(999999, "yhdeksänsataayhdeksänkymmentäyhdeksäntuhattayhdeksänsataayhdeksänkymmentäyhdeksän")]
        public void Convert_ShouldReturnCorrectResult_ForSixDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

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
