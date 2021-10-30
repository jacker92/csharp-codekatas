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

        [Theory]
        [InlineData(1000000, "miljoona")]
        [InlineData(5000000, "viisimiljoonaa")]
        [InlineData(9000000, "yhdeksänmiljoonaa")]
        public void Convert_ShouldReturnCorrectResult_ForEvenSevenDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(1000001, "miljoonayksi")]
        [InlineData(5050505, "viisimiljoonaaviisikymmentätuhattaviisisataaviisi")]
        [InlineData(9999999, "yhdeksänmiljoonaayhdeksänsataayhdeksänkymmentäyhdeksäntuhattayhdeksänsataayhdeksänkymmentäyhdeksän")]
        public void Convert_ShouldReturnCorrectResult_ForSevenDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(10000000, "kymmenenmiljoonaa")]
        [InlineData(50000000, "viisikymmentämiljoonaa")]
        [InlineData(90000000, "yhdeksänkymmentämiljoonaa")]
        public void Convert_ShouldReturnCorrectResult_ForEvenEightDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(10000100, "kymmenenmiljoonaasata")]
        [InlineData(20001001, "kaksikymmentämiljoonaatuhatyksi")]
        [InlineData(55555555, "viisikymmentäviisimiljoonaaviisisataaviisikymmentäviisituhattaviisisataaviisikymmentäviisi")]
        [InlineData(99999999, "yhdeksänkymmentäyhdeksänmiljoonaayhdeksänsataayhdeksänkymmentäyhdeksäntuhattayhdeksänsataayhdeksänkymmentäyhdeksän")]
        public void Convert_ShouldReturnCorrectResult_ForEightDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }

        [Theory]
        [InlineData(100000000, "satamiljoonaa")]
        [InlineData(500000000, "viisisataamiljoonaa")]
        [InlineData(900000000, "yhdeksänsataamiljoonaa")]
        public void Convert_ShouldReturnCorrectResult_ForEvenNineDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }


        [Theory]
        [InlineData(100000001, "satamiljoonaayksi")]
        [InlineData(200010009, "kaksisataamiljoonaakymmenentuhattayhdeksän")]
        [InlineData(555555555, "viisisataaviisikymmentäviisimiljoonaaviisisataaviisikymmentäviisituhattaviisisataaviisikymmentäviisi")]
        [InlineData(999999999, "yhdeksänsataayhdeksänkymmentäyhdeksänmiljoonaayhdeksänsataayhdeksänkymmentäyhdeksäntuhattayhdeksänsataayhdeksänkymmentäyhdeksän")]
        public void Convert_ShouldReturnCorrectResult_ForNineDigitNumbers(int value, string convertedValue)
        {
            var result = _numbersToWordsConverter.Convert(value, Language.Finnish);
            Assert.Equal(convertedValue, result);
        }
    }
}
