namespace BingoKata.Domain
{
    public class BingoCard
    {
        private readonly BingoNumberGenerator _bingoNumberGenerator;

        public BingoCard()
        {
            SpaceRows = new Space[5, 5];
            _bingoNumberGenerator = new BingoNumberGenerator();

            InitializeSpaceRows();
        }

        private void InitializeSpaceRows()
        {
            for (int x = 0; x < 5; x++)
            {
                var bingoRowNumbers = _bingoNumberGenerator.GenerateBingoRow(x);
                for (int y = 0; y < 5; y++)
                {
                    if (x == 2 && y == 2)
                    {
                        SpaceRows[x, y] = new Space();
                        continue;
                    }

                    SpaceRows[x, y] = new Space { Value = bingoRowNumbers.Pop() };
                }
            }
        }

        public Space[,] SpaceRows { get; }
    }
}