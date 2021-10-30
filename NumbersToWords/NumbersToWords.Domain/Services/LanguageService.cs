using NumbersToWords.Domain.LanguageFeatures;
using System.Collections.Generic;

namespace NumbersToWords.Domain.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly List<ILanguage> _languages = new List<ILanguage>
        {
            new EnglishLanguage(),
            new FinnishLanguage()
        };

        public IList<ILanguage> Languages => _languages;
    }
}
