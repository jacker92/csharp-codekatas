namespace FluentCalculator.Domain
{
    public class Calculator
    {
        private int _seed;
        private Stack<int> _values = new Stack<int>();
        private Stack<int> _undoedValues = new Stack<int>();

        public Calculator Seed(int value)
        {
            _seed = value;
            return this;
        }

        public int Result()
        {
            return _seed + _values.Sum();
        }

        public Calculator Plus(int toAdd)
        {
            _values.Push(toAdd);
            return this;
        }

        public Calculator Undo()
        {
            if (_values.Any())
            {
                _undoedValues.Push(_values.Pop());
            }

            return this;
        }

        public Calculator Redo()
        {
            if (_undoedValues.Any())
            {
                _values.Push(_undoedValues.Pop());
            }

            return this;
        }
    }
}