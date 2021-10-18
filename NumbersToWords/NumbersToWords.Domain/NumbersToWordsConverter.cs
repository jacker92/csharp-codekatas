using System;
using System.Collections.Generic;
using System.Text;

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
        };

        public string Convert(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            var valueStr = value.ToString();
            var builder = new StringBuilder();

            if (value < 100 && value > 19)
            {
                var tens = valueStr.Substring(0, 1);
                var asInt = int.Parse($"{tens}0");
                builder.Append(_dictionary[asInt]);
                var ones = valueStr.Substring(1, 1);
                if (ones != "0")
                {
                    builder.Append($"-{_dictionary[int.Parse(ones)]}");
                }
            }

            if (builder.Length > 0)
            {
                return builder.ToString();
            }

            if (value < 21)
            {
                return _dictionary[value];
            }
            return "zero";
        }
    }
}
