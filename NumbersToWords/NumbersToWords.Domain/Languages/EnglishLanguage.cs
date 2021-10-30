using NumbersToWords.Domain.LanguageFeatures;

namespace NumbersToWords.Domain.Languages
{
    public class EnglishLanguage : LanguageBase
    {
        public override Language Language => Language.English;
        public override string Zero => "zero";
        public override string One => "one";
        public override string Two => "two";
        public override string Three => "three";
        public override string Four => "four";
        public override string Five => "five";
        public override string Six => "six";
        public override string Seven => "seven";
        public override string Eight => "eight";
        public override string Nine => "nine";
        public override string Ten => "ten";
        public override string Eleven => "eleven";
        public override string Twelve => "twelve";
        public override string Thirteen => "thirteen";
        public override string Fourteen => "fourteen";
        public override string Fiveteen => "fifteen";
        public override string Sixteen => "sixteen";
        public override string Seventeen => "seventeen";
        public override string Eighteen => "eighteen";
        public override string Nineteen => "nineteen";
        public override string Twenty => "twenty";
        public override string Thirty => "thirty";
        public override string Fourty => "fourty";
        public override string Fifty => "fifty";
        public override string Sixty => "sixty";
        public override string Seventy => "seventy";
        public override string Eighty => "eighty";
        public override string Ninety => "ninety";
        public override string Hundred => "hundred";
        public override string Thousand => "thousand";
        public override string Million => "million";
        public override ILanguageFeatures LanguageSpecificFeatures => new EnglishLanguageFeatures();
    }
}
