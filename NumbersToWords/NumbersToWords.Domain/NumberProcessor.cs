namespace NumbersToWords.Domain
{
    public class NumberProcessor
    {
        public int GetOneDigitNumber(int input)
        {
            return input % 10;
        }
    }
}
