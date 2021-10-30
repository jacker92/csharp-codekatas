namespace NumbersToWords.Domain.LanguageFeatures
{
    public class FinnishLanguageFeatures : ILanguage
    {
        public Language Language => Language.Finnish;
        public bool UsesDashes => false;
        public bool SingleUnitIsSpecifiedAsADigit => false;
        public bool UsesSpacesBetweenNumbers => false;
        public bool UsesPluralizedForms => true;
        public string PluralizedForm(string digits)
        {
            if (digits == Translations.FinnishTranslations[Constants.Thousand])
            {
                return "ta";
            }
            return "a";
        }
    }
}
