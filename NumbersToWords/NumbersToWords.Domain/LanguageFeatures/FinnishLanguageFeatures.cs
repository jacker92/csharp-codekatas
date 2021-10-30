namespace NumbersToWords.Domain.LanguageFeatures
{
    public class FinnishLanguageFeatures : ILanguageFeatures
    {
        public Language Language => Language.Finnish;
        public bool UsesDashes => false;
    }
}
