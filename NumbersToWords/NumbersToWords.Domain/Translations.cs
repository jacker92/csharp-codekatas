using System.Collections.Generic;

namespace NumbersToWords.Domain
{
    public static class Translations
    {
        public static readonly Dictionary<int, string> EnglishTranslations = new Dictionary<int, string>
        {
            {0, "zero"},
            {1, "one"},
            {2, "two"},
            {3, "three"},
            {4, "four"},
            {5, "five"},
            {6, "six"},
            {7, "seven"},
            {8, "eight"},
            {9, "nine"},
            {10, "ten"},
            {11, "eleven"},
            {12, "twelve"},
            {13, "thirteen"},
            {14, "fourteen"},
            {15, "fifteen"},
            {16, "sixteen"},
            {17, "seventeen"},
            {18, "eighteen"},
            {19, "nineteen"},
            {20, "twenty"},
            {30, "thirty"},
            {40, "fourty"},
            {50, "fifty"},
            {60, "sixty"},
            {70, "seventy"},
            {80, "eighty"},
            {90, "ninety"},
            {100, "hundred"},
            {1000, "thousand"},
            {1000000, "million"},
        };

        public static readonly Dictionary<int, string> FinnishTranslations = new Dictionary<int, string>
        {
            {0, "nolla"},
            {1, "yksi"},
            {2, "kaksi"},
            {3, "kolme"},
            {4, "neljä"},
            {5, "viisi"},
            {6, "kuusi"},
            {7, "seitsemän"},
            {8, "kahdeksan"},
            {9, "yhdeksän"},
            {10, "kymmenen"},
            {11, "yksitoista"},
            {12, "kaksitoista"},
            {13, "kolmetoista"},
            {14, "neljätoista"},
            {15, "viisitoista"},
            {16, "kuusitoista"},
            {17, "seitsemäntoista"},
            {18, "kahdeksantoista"},
            {19, "yhdeksäntoista"},
            {20, "kaksikymmentä"},
            {30, "kolmekymmentä"},
            {40, "neljäkymmentä"},
            {50, "viisikymmentä"},
            {60, "kuusikymmentä"},
            {70, "seitsemänkymmentä"},
            {80, "kahdeksankymmentä"},
            {90, "yhdeksänkymmentä"},
            {100, "sata"},
            {1000, "tuhat"},
            {1000000, "miljoona"},
        };
    }
}
