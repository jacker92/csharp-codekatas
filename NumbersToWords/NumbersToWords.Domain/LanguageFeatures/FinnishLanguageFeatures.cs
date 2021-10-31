using System;

namespace NumbersToWords.Domain.LanguageFeatures
{
    public class FinnishLanguageFeatures : ILanguageFeatures
    {
        public bool UsesDashes => false;
        public bool SingleUnitIsSpecifiedAsADigit => false;
        public bool UsesSpacesBetweenNumbers => false;
        public bool UsesPluralizedForms => true;
        public bool UsesSpacesBetweenNumberGroups => false;

        public string SpecialCaseForSingleUnitForMillionOrOver => throw new NotSupportedException();

        public bool UsesSpecialCaseForSingleUnitForMillionOrOver => false;

        public bool UsesSpacesBetweenNumbersMillionAndOver => false;

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
