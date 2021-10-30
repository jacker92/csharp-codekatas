namespace NumbersToWords.Domain.LanguageFeatures
{
    public class FinnishLanguageFeatures : ILanguageFeatures
    {
        public bool UsesDashes => false;
        public bool SingleUnitIsSpecifiedAsADigit => false;
        public bool UsesSpacesBetweenNumbers => false;
        public bool UsesPluralizedForms => true;

        public string PluralizedForm(string digits)
        {
            if (digits == "tuhat")
            {
                return "ta";
            }
            return "a";
        }
    }
}
