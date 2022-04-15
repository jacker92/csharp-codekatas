namespace ClamCard.Domain
{
    public class ClamCard
    {
        public double Balance { get; private set; }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            Balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            if (Balance - amount < 0)
            {
                throw new InsufficientBalanceException(amount);
            }

            Balance -= amount;
        }
    }
}