namespace NumbersToWords.Domain.LanguageFeatures
{
    public interface ILanguage : ILanguageTranslation
    {
        Language Language { get; }
        ILanguageFeatures LanguageSpecificFeatures { get; }
    }
}
