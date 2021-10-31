using NumbersToWords.Domain.Languages;

namespace NumbersToWords.Domain.Services
{
    public interface ILanguageFeatureService
    {
        bool UsesDashes(Language language);
        bool SingleUnitIsSpecifiedAsADigit(Language language);
        bool UsesSpacesBetweenNumbers(Language language);
        bool UsesPluralizedForms(Language language);
        string GetPluralizedForm(Language language, string digits);
        bool UsesSpacesBetweenNumberGroups(Language language);
    }
}
