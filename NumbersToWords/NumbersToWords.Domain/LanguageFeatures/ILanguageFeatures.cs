namespace NumbersToWords.Domain.LanguageFeatures
{
    public interface ILanguageFeatures
    {
        Language Language { get; }
        bool UsesDashes { get; }
        bool SingleUnitIsSpecifiedAsADigit { get; }
        bool UsesSpacesBetweenNumbers { get; }
    }
}
