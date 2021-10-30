using System.Collections.Generic;

namespace NumbersToWords.Domain.Languages
{
    public interface ILanguageTranslation
    {
        Dictionary<int, string> Translations { get; }
        string Zero { get; }
        string One { get; }
        string Two { get; }
        string Three { get; }
        string Four { get; }
        string Five { get; }
        string Six { get; }
        string Seven { get; }
        string Eight { get; }
        string Nine { get; }
        string Ten { get; }
        string Eleven { get; }
        string Twelve { get; }
        string Thirteen { get; }
        string Fourteen { get; }
        string Fiveteen { get; }
        string Sixteen { get; }
        string Seventeen { get; }
        string Eighteen { get; }
        string Nineteen { get; }
        string Twenty { get; }
        string Thirty { get; }
        string Fourty { get; }
        string Fifty { get; }
        string Sixty { get; }
        string Seventy { get; }
        string Eighty { get; }
        string Ninety { get; }
        string Hundred { get; }
        string Thousand { get; }
        string Million { get; }
    }
}
