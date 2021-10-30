namespace NumbersToWords.Domain.Services
{
    public interface ILanguageFeatureService
    {
        bool UsesDashes(Language language);
        bool SingleUnitIsSpecifiedAsADigit(Language language);
        bool UsesSpacesBetweenNumbers(Language language);
    }
}
