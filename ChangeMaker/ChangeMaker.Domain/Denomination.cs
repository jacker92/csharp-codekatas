namespace ChangeMaker.Domain
{
    public class Denomination
    {
        public Denomination(int coin, int amount)
        {
            Coin = coin;
            Amount = amount;
        }

        public int Coin { get; }
        public int Amount { get; set; }
    }
}