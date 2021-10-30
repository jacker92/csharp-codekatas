namespace NumbersToWords.Domain
{
    public interface ILanguageFeatures
    {
        Language Language { get; }
        bool UsesDashes { get; }
    }
}
