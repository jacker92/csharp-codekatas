using ClamCard.Domain.Exceptions;

namespace ClamCard.Domain.Models
{
    public class ClamCard
    {
        public ClamCard(double balance = 0)
        {
            Balance = balance;
        }

        public double Balance { get; private set; }
        public IList<JourneyLogEntry> TravellingHistory { get; } = new List<JourneyLogEntry>();

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