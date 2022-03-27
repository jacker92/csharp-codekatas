namespace FluentCalculator.Domain
{
    public class Calculator : ISeededCalculator
    {
        private int _seed;
        private readonly Stack<int> _values;
        private readonly Stack<int> _undoedValues;

        public Calculator()
        {
            _values = new Stack<int>();
            _undoedValues = new Stack<int>();
        }

        public ISeededCalculator Seed(int value)
        {
            _seed = value;
            return this;
        }

        public int Result()
        {
            return _seed + _values.Sum();
        }

        public ISeededCalculator Plus(int toAdd)
        {
            _values.Push(toAdd);
            return this;
        }

        public ISeededCalculator Minus(int toSubstract)
        {
            _values.Push(-toSubstract);
            return this;
        }

        public ISeededCalculator Undo()
        {
            if (_values.Any())
            {
                _undoedValues.Push(_values.Pop());
            }

            return this;
        }

        public ISeededCalculator Redo()
        {
            if (_undoedValues.Any())
            {
                _values.Push(_undoedValues.Pop());
            }

            return this;
        }

        public ISeededCalculator Save()
        {
            _seed += _values.Sum();
            _values.Clear();
            _undoedValues.Clear();

            return this;
        }
    }
}