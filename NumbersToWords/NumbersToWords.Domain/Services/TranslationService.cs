using NumbersToWords.Domain.Languages;
using System;
using System.Linq;

namespace NumbersToWords.Domain.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly ILanguageService _languageService;

        public TranslationService(ILanguageService languageService)
        {
            _languageService = languageService ?? throw new ArgumentNullException(nameof(languageService));
        }

        public string Translate(int numberToTranslate, Language language)
        {
            return _languageService.Languages
                .Single(x => x.Language == language)
                .Translations[numberToTranslate];
        }
    }
}
