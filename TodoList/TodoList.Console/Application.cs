using TodoList.Domain;

namespace TodoList.Console
{
    public class Application
    {
        private readonly IOutput _output;
        private readonly ITodoList _todoList;

        public Application(IOutput output, ITodoList todoList)
        {
            _output = output;
            _todoList = todoList;
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

            if (args[0] == "task")
            {
                _todoList.Add(new TodoItem { Task = args[2], Date = DateTime.Parse(args[4]) });
            }
        }
    }
}