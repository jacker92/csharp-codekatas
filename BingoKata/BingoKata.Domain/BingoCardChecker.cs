namespace BingoKata.Domain
{
    public class BingoCardChecker
    {
        public bool HasBingo(List<int> list, BingoCard bingoCard)
        {
            if (HasVerticalBingo(list, bingoCard))
            {
                return true;
            }

            if (HasHorizontalBingo(list, bingoCard))
            {
                return true;
            }

            if (HasDiagonalBingoFromUpperLeftToLowerRight(list, bingoCard))
            {
                return true;
            }

            if (HasDiagonalBingoFromLowerLeftToUpperRight(list, bingoCard))
            {
                return true;
            }

            return false;
        }

        private bool HasDiagonalBingoFromUpperLeftToLowerRight(List<int> list, BingoCard bingoCard)
        {
            var hasDiagonalBingo = true;
            for (int x = 0; x < 5; x++)
            {
                var current = bingoCard.SpaceRows[x, x];

                if (current.Value.HasValue && !list.Contains(current.Value.Value))
                {
                    hasDiagonalBingo = false;
                    break;
                }
            }

            return hasDiagonalBingo;
        }

        private bool HasDiagonalBingoFromLowerLeftToUpperRight(List<int> list, BingoCard bingoCard)
        {
            var hasDiagonalBingo = true;
            for (int x = 4; x >= 0; x--)
            {
                var current = bingoCard.SpaceRows[x, 4 - x];

                if (current.Value.HasValue && !list.Contains(current.Value.Value))
                {
                    hasDiagonalBingo = false;
                    break;
                }
            }

            return hasDiagonalBingo;
        }


        private static bool HasHorizontalBingo(List<int> list, BingoCard bingoCard)
        {
            for (int y = 0; y < 5; y++)
            {
                var hasHorizontalBingo = true;
                for (int x = 0; x < 5; x++)
                {
                    var current = bingoCard.SpaceRows[x, y];

                    if (current.Value.HasValue && !list.Contains(current.Value.Value))
                    {
                        hasHorizontalBingo = false;
                        break;
                    }
                }

                if (hasHorizontalBingo)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool HasVerticalBingo(List<int> list, BingoCard bingoCard)
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