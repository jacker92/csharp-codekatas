namespace BingoKata.Domain
{
    public class BingoCaller
    {
        private readonly Stack<int> _numbers;

        public BingoCaller()
        {
            _numbers = new Stack<int>();

            InitializeBingoNumbers();
        }

        private void InitializeBingoNumbers()
        {
            var random = new Random();
            var numbers = Enumerable.Range(1, 75).OrderBy(i => random.Next());

            foreach (var number in numbers)
            {
                _numbers.Push(number);
            }
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