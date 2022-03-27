namespace FluentCalculator.Domain
{
    public class Calculator
    {
        private Stack<int> _values = new Stack<int>();
        private Stack<int> _undoedValues = new Stack<int>();

        public Calculator Seed(int value)
        {
            _values.Push(value);
            return this;
        }

        public int Result()
        {
            return _values.Sum();
        }

        public Calculator Plus(int toAdd)
        {
            _values.Push(toAdd);
            return this;
        }

        public Calculator Undo()
        {
            _undoedValues.Push(_values.Pop());
            return this;
        }

        public Calculator Redo()
        {
            _values.Push(_undoedValues.Pop());
            return this;
        }
    }
}