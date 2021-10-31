using NumbersToWords.Domain.LanguageFeatures;

namespace NumbersToWords.Domain.Languages
{
    public class SwedishLanguage : LanguageBase
    {
        public override ILanguageFeatures LanguageSpecificFeatures => new SwedishLanguageFeatures();

        public override string Zero => "noll";

        public override string One => "ett";

        public override string Two => "två";

        public override string Three => "tre";

        public override string Four => "fyra";

        public override string Five => "fem";

        public override string Six => "sex";

        public override string Seven => "sju";

        public override string Eight => "åtta";

        public override string Nine => "nio";

        public override string Ten => "tio";

        public override string Eleven => "elva";

        public override string Twelve => "tolv";

        public override string Thirteen => "tretton";

        public override string Fourteen => "fjorton";

        public override string Fiveteen => "femton";

        public override string Sixteen => "sexton";

        public override string Seventeen => "sjutton";

        public override string Eighteen => "arton";

        public override string Nineteen => "nitton";

        public override string Twenty => "tjugo";

        public override string Thirty => "trettio";

        public override string Fourty => "fyrtio";

        public override string Fifty => "femtio";

        public override string Sixty => "sextio";

        public override string Seventy => "sjuttio";

        public override string Eighty => "åttio";

        public override string Ninety => "nittio";

        public override string Hundred => "hundra";

        public override string Thousand => "tusen";

        public override string Million => "miljon";

        public override Language Language => Language.Swedish;
    }
}
