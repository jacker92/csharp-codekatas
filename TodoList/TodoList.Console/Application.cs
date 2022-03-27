namespace TodoList.Console
{
    public class Application
    {
        private readonly IOutput _output;

        public Application(IOutput output)
        {
            _output = output;
        }

        public void Run(string[] args)
        {
            if (args is null)
            {
                _output.WriteLine(Messages.InvalidArguments);
                return;
            }

            if (args.First() == "?")
            {
                _output.WriteLine(Messages.Instructions);
                return;
            }
        }
    }
}