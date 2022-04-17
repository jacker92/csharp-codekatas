namespace BingoKata.Domain
{
    public class BingoCard
    {
        public BingoCard(int[,] numbers)
        {
            SpaceRows = new Space[5, 5];

            InitializeSpaceRows(numbers);
        }

        private void InitializeSpaceRows(int[,] numbers)
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (x == 2 && y == 2)
                    {
                        SpaceRows[x, y] = new Space();
                        continue;
                    }

                    SpaceRows[x, y] = new Space { Value = numbers[x, y] };
                }
            }
        }

        public Space[,] SpaceRows { get; }
    }
}