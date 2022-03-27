namespace FluentCalculator.Domain
{
    public class Calculator
    {
        private int _value;

        public Calculator Seed(int value)
        {
            _value = value;
            return this;
        }

        public int Result()
        {
            return _value;
        }

        public Calculator Plus(int toAdd)
        {
            _value += toAdd;
            return this;
        }
    }
}