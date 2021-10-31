namespace NumbersToWords.Domain.LanguageFeatures
{
    public class EnglishLanguageFeatures : ILanguageFeatures
    {
        public bool UsesDashes => true;
        public bool SingleUnitIsSpecifiedAsADigit => true;
        public bool UsesSpacesBetweenNumbers => true;
        public bool UsesPluralizedForms => false;
        public bool UsesSpacesBetweenNumberGroups => true;

        public string PluralizedForm(string digits)
        {
            return string.Empty;
        }
    }
}
