using System.Collections.Generic;

namespace NumbersToWords.Domain.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly Dictionary<Language, Dictionary<int, string>> _dictionaries = new Dictionary<Language, Dictionary<int, string>> {
            { Language.English, Translations.EnglishTranslations },
            { Language.Finnish, Translations.FinnishTranslations } };

        public string Translate(int numberToTranslate, Language language)
        {
            return _dictionaries[language][numberToTranslate];
        }
    }
}
