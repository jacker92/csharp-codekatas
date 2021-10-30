namespace NumbersToWords.Domain.Language.LanguageFeatures
{
    public interface ILanguageFeatures
    {
        Language Language { get; }
        bool UsesDashes { get; }
    }
}
