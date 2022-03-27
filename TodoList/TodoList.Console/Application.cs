using CommandLine;
using TodoList.Domain;

namespace TodoList.Console
{
    public class Options
    {
        [Value(0, Required = true, MetaName = "operation", HelpText = "Operation for the todo items")]
        public string Operation { get; set; }

        [Option('t', "name", Required = false, HelpText = "Task name")]
        public string TaskName { get; set; }
        
        [Option('d', "date", Required = false, HelpText = "Task due date")]
        public DateTime DueDate { get; set; }
    }

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
            var arguments = Parser.Default.ParseArguments<Options>(args);

            arguments.WithParsed(options =>
            {
                RunWithParsedArguments(options);
            })
            .WithNotParsed(error =>
            {

            });
        }

        private void RunWithParsedArguments(Options obj)
        {
            //if (args is null)
            //{
            //    _output.WriteLine(Messages.InvalidArguments);
            //    return;
            //}

            //if (args.First() == "?")
            //{
            //    _output.WriteLine(Messages.Instructions);
            //    return;
            //}

            //if (args[0] == "task")
            //{
            //    _todoList.Add(new TodoItem { Task = args[2], Date = DateTime.Parse(args[4]) });
            //}
        }
    }
}