namespace ClamCard.Domain.Exceptions
{

    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(double amount) : base($"Cannot withdraw amount {amount} because user does not have enough balance.")
        {
        }
    }
}