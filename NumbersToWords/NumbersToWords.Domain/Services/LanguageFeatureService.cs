using NumbersToWords.Domain.LanguageFeatures;
using System.Collections.Generic;
using System.Linq;

namespace NumbersToWords.Domain.Services
{
    public class LanguageFeatureService : ILanguageFeatureService
    {
        private readonly List<ILanguage> _languageFeatures = new List<ILanguage>
        {
            new EnglishLanguageFeatures(),
            new FinnishLanguageFeatures()
        };

        public bool SingleUnitIsSpecifiedAsADigit(Language language)
        {
            return _languageFeatures.Single(x => x.Language == language)
                                   .SingleUnitIsSpecifiedAsADigit;
        }

        public bool UsesDashes(Language language)
        {
            return _languageFeatures.Single(x => x.Language == language)
                                    .UsesDashes;
        }

        public bool UsesPluralizedForms(Language language)
        {
            return _languageFeatures.Single(x => x.Language == language)
                                    .UsesPluralizedForms;
        }

        public bool UsesSpacesBetweenNumbers(Language language)
        {
            return _languageFeatures.Single(x => x.Language == language)
                                 .UsesSpacesBetweenNumbers;
        }

        public string GetPluralizedForm(Language language, string digits)
        {
            return _languageFeatures.Single(x => x.Language == language)
                                .PluralizedForm(digits);
        }
    }
}
