namespace NumbersToWords.Domain.LanguageFeatures
{
    public class SwedishLanguageFeatures : ILanguageFeatures
    {
        public bool UsesDashes => false;

        public bool SingleUnitIsSpecifiedAsADigit => true;

        public bool UsesSpacesBetweenNumbers => true;

        public bool UsesPluralizedForms => true;

        public string PluralizedForm(string digits)
        {
            throw new System.NotImplementedException();
        }
    }
}
