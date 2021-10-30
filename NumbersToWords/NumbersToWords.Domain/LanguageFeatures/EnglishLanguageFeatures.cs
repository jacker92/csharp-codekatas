namespace NumbersToWords.Domain.LanguageFeatures
{
    public class EnglishLanguageFeatures : ILanguageFeatures
    {
        public Language Language => Language.English;
        public bool UsesDashes => true;
    }
}
