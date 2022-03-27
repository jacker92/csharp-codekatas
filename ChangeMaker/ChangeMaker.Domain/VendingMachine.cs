namespace ChangeMaker.Domain
{
    public class VendingMachine
    {
        public VendingMachine(int[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }
        }

        public int[] CalculateChange(double purchaseAmount, double tenderAmount)
        {
            return new int[] { 25, 25, 25 };
        }
    }
}