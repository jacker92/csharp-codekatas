namespace NumbersToWords.Domain
{
    public interface ILanguageFeatureService
    {
        bool UsesDashes(Language language);
    }
}
