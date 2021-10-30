namespace NumbersToWords.Domain.LanguageFeatures
{
    public interface ILanguage
    {


        Language Language { get; }
        bool UsesDashes { get; }
        bool SingleUnitIsSpecifiedAsADigit { get; }
        bool UsesSpacesBetweenNumbers { get; }
        bool UsesPluralizedForms { get; }
        string PluralizedForm(string digits);
    }
}
