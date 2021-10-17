using BalancedBrackets.Domain.Models;

namespace BalancedBrackets.Domain
{
    public interface IBracketResultConverter
    {
        string ConvertBracketParsingResult(BracketParsingResult bracketParsingResult);
    }
}