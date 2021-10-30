namespace NumbersToWords.Domain.LanguageFeatures
{
    public class FinnishLanguageFeatures : ILanguageFeatures
    {
        public Language Language => Language.Finnish;
        public bool UsesDashes => false;
        public bool SingleUnitIsSpecifiedAsADigit => false;
        public bool UsesSpacesBetweenNumbers => false;
        public bool UsesPluralizedForms => true;
        public string PluralizedForm(int digits)
        {
            if (digits > 1000)
            {
                return "ta";
            }
            return "a";
        }
    }
}
