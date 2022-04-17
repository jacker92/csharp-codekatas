namespace BingoKata.Domain
{
    public class BingoNumberGenerator
    {
        private readonly Random _random;

        public BingoNumberGenerator()
        {
            _random = new Random();
        }

        public Stack<int> GenerateNumbers()
        {
            var numbers = new Stack<int>();

            var numbersShuffled = Enumerable.Range(1, 75).OrderBy(i => _random.Next());

            foreach (var number in numbersShuffled)
            {
                numbers.Push(number);
            }

            return numbers;
        }

        public Stack<int> GenerateBingoRow(int row)
        {
            var numbersShuffled = Enumerable.Range(Boundaries.Get(row), 15).OrderBy(i => _random.Next());

            var stack = new Stack<int>();

            foreach (var item in numbersShuffled.Take(5))
            {
                stack.Push(item);
            }

            return stack;
        }
    }
}