using NumbersToWords.Domain.LanguageFeatures;

namespace NumbersToWords.Domain.Languages
{
    public interface ILanguage : ILanguageTranslation
    {
        Language Language { get; }
        ILanguageFeatures LanguageSpecificFeatures { get; }
    }
}
