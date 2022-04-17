namespace BingoKata.Domain
{
    public class BingoCaller
    {
        private readonly Stack<int> _numbers;
        private readonly BingoNumberGenerator _bingoNumberGenerator;

        public BingoCaller(BingoNumberGenerator bingoNumberGenerator)
        {
            _bingoNumberGenerator = bingoNumberGenerator;
           _numbers = _bingoNumberGenerator.GenerateNumbers();
        }

        public int CallNumber()
        {
            if (!_numbers.TryPop(out int result))
            {
                throw new NoNumbersLeftException();
            }

            return result;
        }
    }
}