namespace NumbersToWords.Domain.Language.LanguageFeatures
{
    public class EnglishLanguageFeatures : ILanguageFeatures
    {
        public Language Language => Language.English;
        public bool UsesDashes => true;
    }
}
