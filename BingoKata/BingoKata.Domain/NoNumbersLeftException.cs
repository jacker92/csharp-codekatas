namespace BingoKata.Domain
{
    public class NoNumbersLeftException : Exception
    {
        public NoNumbersLeftException() : base("No numbers left!")
        {
        }
    }
}