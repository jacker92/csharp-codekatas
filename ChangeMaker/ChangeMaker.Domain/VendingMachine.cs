namespace ChangeMaker.Domain
{
    public class VendingMachine
    {
        private readonly int[] _values;

        public VendingMachine(int[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            _values = values
                .OrderByDescending(x => x)
                .ToArray();
        }

        public int[] CalculateChange(double purchaseAmount, double tenderAmount)
        {
            var change = Convert.ToInt32((tenderAmount - purchaseAmount) * 100);
            var coins = new List<int>();

            while (change > 0)
            {
                var result = _values.First(x => x <= change);
                coins.Add(result);
                change -= result;
            }

            return coins.ToArray();
        }
    }
}