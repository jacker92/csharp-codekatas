using System;
using System.Collections.Generic;

namespace NumbersToWords.Domain
{
    public class NumbersToWordsConverter
    {
        private readonly Dictionary<int, string> _dictionary = new Dictionary<int, string>
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
        };

        private readonly NumberProcessor _numberProcessor;

        public NumbersToWordsConverter()
        {
            _numberProcessor = new NumberProcessor();
        }

        public string Convert(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            if (value == 0)
            {
                return _dictionary[0];
            }

            var values = new List<string>();

            values.AddRange(ParseFourDigitNumbers(value));
            values.AddRange(ParseThreeDigitNumbers(value));
            values.AddRange(ParseTwoDigitNumbers(value));

            return string.Join(' ', values);
        }

        private IList<string> ParseFourDigitNumbers(int value)
        {
            var list = new List<string>();

            if (value >= 1000)
            {
                var oneDigit = _numberProcessor.GetFirstDigit(value);
                list.Add($"{_dictionary[oneDigit]} {_dictionary[1000]}");
            }

            return list;
        }

        private IList<string> ParseThreeDigitNumbers(int value)
        {
            var list = new List<string>();

            if (value < 1000 && value >= 100)
            {
                var oneDigit = _numberProcessor.GetFirstDigit(value);
                list.Add($"{_dictionary[oneDigit]} {_dictionary[100]}");
            }

            return list;
        }

        private IList<string> ParseTwoDigitNumbers(int value)
        {
            var twoDigits = _numberProcessor.GetTwoDigitNumber(value);
            var values = ParseTwoDigitsOverTwenty(twoDigits);

            if (twoDigits < 21 && twoDigits != 0)
            {
                values.Add(_dictionary[twoDigits]);
            }

            return values;
        }

        private List<string> ParseTwoDigitsOverTwenty(int twoDigits)
        {
            var values = new List<string>();

            if (twoDigits > 20)
            {
                var lastDigit = _numberProcessor.GetLastDigit(twoDigits);
                var evenTwoDigitNumber = _numberProcessor.GetEvenTwoDigitNumber(twoDigits);

                var evenTwoDigitNumberStr = _dictionary[evenTwoDigitNumber];

                if (lastDigit != 0)
                {
                    evenTwoDigitNumberStr += $"-{_dictionary[lastDigit]}";
                }

                values.Add(evenTwoDigitNumberStr);
            }

            return values;
        }
    }
}
