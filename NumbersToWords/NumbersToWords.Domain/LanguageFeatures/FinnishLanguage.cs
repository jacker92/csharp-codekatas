namespace NumbersToWords.Domain.LanguageFeatures
{
    public class FinnishLanguage : LanguageBase
    {
        public override Language Language => Language.Finnish;

        public override string Zero => "nolla";

        public override string One => "yksi";

        public override string Two => "kaksi";

        public override string Three => "kolme";

        public override string Four => "neljä";

        public override string Five => "viisi";

        public override string Six => "kuusi";

        public override string Seven => "seitsemän";

        public override string Eight => "kahdeksan";

        public override string Nine => "yhdeksän";

        public override string Ten => "kymmenen";

        public override string Eleven => "yksitoista";

        public override string Twelve => "kaksitoista";

        public override string Thirteen => "kolmetoista";

        public override string Fourteen => "neljätoista";

        public override string Fiveteen => "viisitoista";

        public override string Sixteen => "kuusitoista";

        public override string Seventeen => "seitsemäntoista";

        public override string Eighteen => "kahdeksantoista";

        public override string Nineteen => "yhdeksäntoista";

        public override string Twenty => "kaksikymmentä";

        public override string Thirty => "kolmekymmentä";

        public override string Fourty => "neljäkymmentä";

        public override string Fifty => "viisikymmentä";

        public override string Sixty => "kuusikymmentä";

        public override string Seventy => "seitsemänkymmentä";

        public override string Eighty => "kahdeksankymmentä";

        public override string Ninety => "yhdeksänkymmentä";

        public override string Hundred => "sata";

        public override string Thousand => "tuhat";

        public override string Million => "miljoona";

        public override ILanguageFeatures LanguageSpecificFeatures => new FinnishLanguageFeatures();
    }
}
