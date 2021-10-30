namespace NumbersToWords.Domain.Services
{
    public interface ILanguageFeatureService
    {
        bool UsesDashes(Language language);
    }
}
