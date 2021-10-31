using NumbersToWords.Domain.LanguageFeatures;
using NumbersToWords.Domain.Languages;
using System;
using System.Linq;

namespace NumbersToWords.Domain.Services
{
    public class LanguageFeatureService : ILanguageFeatureService
    {
        private readonly ILanguageService _languageService;

        public LanguageFeatureService(ILanguageService languageService)
        {
            _languageService = languageService ?? throw new ArgumentNullException(nameof(languageService));
        }

        public bool SingleUnitIsSpecifiedAsADigit(Language language)
        {
            return GetLanguageFeature(language).SingleUnitIsSpecifiedAsADigit;
        }

        public bool UsesDashes(Language language)
        {
            return GetLanguageFeature(language).UsesDashes;
        }

        public bool UsesPluralizedForms(Language language)
        {
            return GetLanguageFeature(language).UsesPluralizedForms;
        }

        public bool UsesSpacesBetweenNumbers(Language language)
        {
            return GetLanguageFeature(language).UsesSpacesBetweenNumbers;
        }

        public string GetPluralizedForm(Language language, string digits)
        {
            return GetLanguageFeature(language).PluralizedForm(digits);
        }

        private ILanguageFeatures GetLanguageFeature(Language language)
        {
            return _languageService.Languages
                .Single(x => x.Language == language)
                .LanguageSpecificFeatures;
        }

        public bool UsesSpacesBetweenNumberGroups(Language language)
        {
            return GetLanguageFeature(language).UsesSpacesBetweenNumberGroups;
        }
    }
}
