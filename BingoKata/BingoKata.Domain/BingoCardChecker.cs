namespace BingoKata.Domain
{
    public class BingoCardChecker
    {
        public bool HasBingo(List<int> list, BingoCard bingoCard)
        {
            for (int x = 0; x < 5; x++)
            {
                var isBingo = true;
                for (int y = 0; y < 5; y++)
                {
                    var current = bingoCard.SpaceRows[x, y];

                    if (current.Value.HasValue && !list.Contains(current.Value.Value))
                    {
                        isBingo = false;
                        break;
                    }
                }

                if (isBingo)
                {
                    return true;
                }
            }


            return false;
        }
    }
}