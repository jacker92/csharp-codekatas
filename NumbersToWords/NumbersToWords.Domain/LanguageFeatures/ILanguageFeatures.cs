namespace NumbersToWords.Domain.LanguageFeatures
{
    public interface ILanguageFeatures
    {
        Language Language { get; }
        bool UsesDashes { get; }
    }
}
