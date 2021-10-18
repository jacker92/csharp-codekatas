namespace NumbersToWords.Domain
{
    public class NumberProcessor
    {
        public int GetOneDigitNumber(int input)
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
    }
}
