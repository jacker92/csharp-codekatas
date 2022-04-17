namespace BingoKata.Domain
{
    public class BingoCardGenerator
    {
        private readonly BingoNumberGenerator _bingoNumberGenerator;

        public BingoCardGenerator(BingoNumberGenerator bingoNumberGenerator)
        {
            _bingoNumberGenerator = bingoNumberGenerator;
        }

        public BingoCard Generate()
        {
            int[,] numbers = GenerateNumbers();

            return new BingoCard(numbers);
        }

        private int[,] GenerateNumbers()
        {
            var numbers = new int[5, 5];

            for (int x = 0; x < 5; x++)
            {
                var row = _bingoNumberGenerator.GenerateBingoRow(x);
                for (int y = 0; y < 5; y++)
                {
                    numbers[x, y] = row.Pop();
                }
            }

            return numbers;
        }
    }
}