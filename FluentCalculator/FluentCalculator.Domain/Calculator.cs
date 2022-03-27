namespace FluentCalculator.Domain
{
    public class Calculator
    {
        private Stack<int> _stack = new Stack<int>();

        public Calculator Seed(int value)
        {
            _stack.Push(value);
            return this;
        }

        public int Result()
        {
            return _stack.Sum();
        }

        public Calculator Plus(int toAdd)
        {
            _stack.Push(toAdd);
            return this;
        }

        public Calculator Undo()
        {
            _stack.Pop();
            return this;
        }
    }
}