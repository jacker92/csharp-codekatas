namespace NumbersToWords.Domain.LanguageFeatures
{
    public class SwedishLanguageFeatures : ILanguageFeatures
    {
        public bool UsesDashes => false;

        public bool SingleUnitIsSpecifiedAsADigit => true;

        public bool UsesSpacesBetweenNumbers => false;

        public bool UsesPluralizedForms => true;

        public bool UsesSpacesBetweenNumberGroups => true;

        public string PluralizedForm(string digits)
        {
            return string.Empty;
        }
    }
}
