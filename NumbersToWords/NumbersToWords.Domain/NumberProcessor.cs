namespace NumbersToWords.Domain
{
    public class NumberProcessor
    {
        public int GetLastDigit(int input)
        {
            return input % 10;
        }

        public int GetTwoDigitNumber(int input)
        {
            return input % 100;
        }

        public int GetEvenTwoDigitNumber(int input)
        {
            return input % 100 - input % 10;
        }

        public int GetEvenThreeDigitNumber(int input)
        {
            return input % 1000 - input % 100;
        }

        public int GetFirstDigit(int input)
        {
            if (input >= 100000000) input /= 100000000;
            if (input >= 10000) input /= 10000;
            if (input >= 100) input /= 100;
            if (input >= 10) input /= 10;

            return input;
        }

        public int GetThreeDigitNumber(int value)
        {
            return value % 1000;
        }

        public int GetEvenFourDigitNumber(int input)
        {
            return input % 10000 - input % 1000;
        }

        public int GetFourDigitNumber(int input)
        {
            return input % 10000;
        }

        public int GetAmountOfThousands(int input)
        {
            return GetAmountOf(input, 3);
        }

        public int GetAmountOfMillions(int input)
        {
            return GetAmountOf(input, 6);
        }

        private int GetAmountOf(int input, int indexOfEndChar)
        {
            var str = input.ToString();
            if (str.Length <= indexOfEndChar)
            {
                return 0;
            }

            var endIndex = str.Length - indexOfEndChar;

            if (endIndex - 3 > 0)
            {
                str = str.Remove(0, endIndex - 3);
                endIndex = str.Length - indexOfEndChar;
            }

            var startIndex = endIndex - 3 < 0 ? 0 : endIndex - 3;
            var calculatedEndIndex = str.Length % 3 == 0 ? 3 : str.Length % 3;

            str = str.Substring(startIndex, calculatedEndIndex);
            return int.Parse(str);
        }
    }
}
