using NumbersToWords.Domain.Languages;
using System.Collections.Generic;

namespace NumbersToWords.Domain.Services
{
    public interface INumberParserService
    {
        IList<string> ParseFourFiveAndSixDigitNumbers(int value, Language language);
        IList<string> ParseSevenEightAndNineDigitNumbers(int value, Language language);
        string ParseTwoDigitNumbers(int value, Language language);
        string ParseThreeDigitNumbers(int value, Language language);
    }
}