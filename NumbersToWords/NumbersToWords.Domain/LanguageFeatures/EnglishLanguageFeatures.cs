namespace NumbersToWords.Domain.LanguageFeatures
{
    public class EnglishLanguageFeatures : ILanguage
    {
        public Language Language => Language.English;
        public bool UsesDashes => true;
        public bool SingleUnitIsSpecifiedAsADigit => true;
        public bool UsesSpacesBetweenNumbers => true;
        public bool UsesPluralizedForms => false;

        public string PluralizedForm(string digits)
        {
            return string.Empty;
        }
    }
}
