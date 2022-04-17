namespace BingoKata.Domain
{
    public class BingoCaller
    {
        private readonly Random _random = new Random();
        public int CallNumber()
        {
            return _random.Next(1, 75);
        }
    }
}