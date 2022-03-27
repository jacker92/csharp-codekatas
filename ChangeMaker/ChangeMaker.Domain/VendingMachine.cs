namespace ChangeMaker.Domain
{
    public class VendingMachine
    {
        private readonly int[]? _values;
        private readonly Denomination[]? _denominations;

        public VendingMachine(IEnumerable<Denomination> denominations)
        {
            if (denominations is null)
            {
                throw new ArgumentNullException(nameof(denominations));
            }

            _denominations = denominations
                .OrderByDescending(x => x.Coin)
                .ToArray();
        }

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
            if (_values == null)
            {
                return CalculateFixedChange(purchaseAmount, tenderAmount);
            }

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

        private int[] CalculateFixedChange(double purchaseAmount, double tenderAmount)
        {
            var change = Convert.ToInt32((tenderAmount - purchaseAmount) * 100);
            var coins = new List<int>();

            while (change > 0)
            {
                var result = _denominations!.First(x => x.Coin <= change);
                coins.Add(result.Coin);
                change -= result.Coin;
            }

            return coins.ToArray();
        }
    }
}