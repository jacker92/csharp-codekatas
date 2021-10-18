using System;

namespace NumbersToWords.Domain
{
    public class NumbersToWordsConverter
    {
        public string Convert(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            return "zero";
        }
    }
}
