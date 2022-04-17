namespace BingoKata.Domain
{
    public static class Boundaries
    {
        public static int Get(int row)
        {
            switch (row)
            {
                case 0:
                    return 1;
                case 1:
                    return 16;
                case 2:
                    return 31;
                case 3:
                    return 46;
                default:
                    return 61;
            }
        }
    }
}