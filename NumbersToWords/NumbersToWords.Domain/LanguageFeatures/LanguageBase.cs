using System.Collections.Generic;

namespace NumbersToWords.Domain.LanguageFeatures
{
    public abstract class LanguageBase : ILanguage
    {
        public LanguageBase()
        {
            Translations = new Dictionary<int, string>
        {
            {0, Zero},
            {1, One},
            {2, Two},
            {3, Three},
            {4, Four},
            {5, Five},
            {6, Six},
            {7, Seven},
            {8, Eight},
            {9, Nine},
            {10, Ten},
            {11, Eleven},
            {12, Twelve},
            {13, Thirteen},
            {14, Fourteen},
            {15, Fiveteen},
            {16, Sixteen},
            {17, Seventeen},
            {18, Eighteen},
            {19, Nineteen},
            {20, Twenty},
            {30, Thirty},
            {40, Fourty},
            {50, Fifty},
            {60, Sixty},
            {70, Seventy},
            {80, Eighty},
            {90, Ninety},
            {100, Hundred},
            {1000, Thousand},
            {1000000, Million},
        };
        }
        public Dictionary<int, string> Translations { get; }
        public abstract ILanguageFeatures LanguageSpecificFeatures { get; }
        public abstract string Zero { get; }
        public abstract string One { get; }
        public abstract string Two { get; }
        public abstract string Three { get; }
        public abstract string Four { get; }
        public abstract string Five { get; }
        public abstract string Six { get; }
        public abstract string Seven { get; }
        public abstract string Eight { get; }
        public abstract string Nine { get; }
        public abstract string Ten { get; }
        public abstract string Eleven { get; }
        public abstract string Twelve { get; }
        public abstract string Thirteen { get; }
        public abstract string Fourteen { get; }
        public abstract string Fiveteen { get; }
        public abstract string Sixteen { get; }
        public abstract string Seventeen { get; }
        public abstract string Eighteen { get; }
        public abstract string Nineteen { get; }
        public abstract string Twenty { get; }
        public abstract string Thirty { get; }
        public abstract string Fourty { get; }
        public abstract string Fifty { get; }
        public abstract string Sixty { get; }
        public abstract string Seventy { get; }
        public abstract string Eighty { get; }
        public abstract string Ninety { get; }
        public abstract string Hundred { get; }
        public abstract string Thousand { get; }
        public abstract string Million { get; }
        public abstract Language Language { get; }
    }
}
