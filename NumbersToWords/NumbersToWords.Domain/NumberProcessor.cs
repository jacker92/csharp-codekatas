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
    }
}
