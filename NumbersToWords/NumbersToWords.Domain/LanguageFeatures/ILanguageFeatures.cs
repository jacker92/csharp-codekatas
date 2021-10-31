using NumbersToWords.Domain.Languages;

namespace NumbersToWords.Domain.LanguageFeatures
{
    public interface ILanguageFeatures
    {
        bool UsesDashes { get; }
        bool SingleUnitIsSpecifiedAsADigit { get; }
        bool UsesSpacesBetweenNumbers { get; }
        bool UsesPluralizedForms { get; }
        string PluralizedForm(string digits);
        bool UsesSpacesBetweenNumberGroups { get; }
        string SpecialCaseForSingleUnitForMillionOrOver { get; }
        bool UsesSpecialCaseForSingleUnitForMillionOrOver { get; }
        bool UsesSpacesBetweenNumbersMillionAndOver { get; }
    }
}
