using BalancedBrackets.Domain.Models;
using System;
using System.Collections.Generic;

namespace BalancedBrackets.Domain
{
    public class BracketParser
    {
        private readonly IBracketResultConverter _bracketResultConverter;
        private readonly Stack<char> _brackets;

        public BracketParser(IBracketResultConverter bracketResultConverter)
        {
            _bracketResultConverter = bracketResultConverter;
            _brackets = new Stack<char>();
        }

        public string Parse(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Equals(string.Empty))
            {
                return string.Empty;
            }

            foreach (var c in input)
            {
                if (c == '[')
                {
                    _brackets.Push(c);
                }
                else if (c == ']' && !_brackets.TryPop(out _))
                {
                    return _bracketResultConverter.ConvertBracketParsingResult(BracketParsingResult.Fail);
                }
            }

            return _brackets.Count == 0 ?
                 _bracketResultConverter.ConvertBracketParsingResult(BracketParsingResult.Ok) :
                 _bracketResultConverter.ConvertBracketParsingResult(BracketParsingResult.Fail);
        }
    }
}
