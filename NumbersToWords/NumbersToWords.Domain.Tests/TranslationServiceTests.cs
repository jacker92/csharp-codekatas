using NumbersToWords.Domain.Languages;
using NumbersToWords.Domain.Services;
using System;
using Xunit;

namespace NumbersToWords.Domain.Tests
{
    public class TranslationServiceTests
    {
        private readonly TranslationService _translationService;

        public TranslationServiceTests()
        {
            _translationService = new TranslationService(new LanguageService());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        [InlineData(16)]
        [InlineData(17)]
        [InlineData(18)]
        [InlineData(19)]
        [InlineData(20)]
        [InlineData(30)]
        [InlineData(40)]
        [InlineData(50)]
        [InlineData(60)]
        [InlineData(70)]
        [InlineData(80)]
        [InlineData(90)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(1000000)]
        public void Translate_ShouldWork_ForAllLanguages(int value)
        {
            foreach (var language in Enum.GetValues(typeof(Language)))
            {
                var result = _translationService.Translate(value, (Language)language);
                Assert.NotNull(result);
            }
        }
    }
}
