namespace NumbersToWords.Domain
{
    public class FinnishLanguageFeatures : ILanguageFeatures
    {
        public Language Language => Language.Finnish;
        public bool UsesDashes => false;
    }
}
