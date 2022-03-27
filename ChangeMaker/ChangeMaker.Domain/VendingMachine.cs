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
    }
}