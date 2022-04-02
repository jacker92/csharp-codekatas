using CommandLine;
using System.Text;
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
            if (args is null)
            {
                _output.WriteLine(Messages.InvalidArguments);
                return;
            }

            var possibleHelpText = new StringWriter();
            var arguments = ParseArguments(args, possibleHelpText);
            Run(possibleHelpText, arguments);
        }

        private static ParserResult<Options> ParseArguments(string[] args, StringWriter stringWriter)
        {
            var parser = new Parser(config => config.HelpWriter = stringWriter);
            var arguments = parser.ParseArguments<Options>(args);
            return arguments;
        }

        private void Run(StringWriter stringWriter, ParserResult<Options> arguments)
        {
            arguments.WithParsed(options => RunWithParsedArguments(options))
                     .WithNotParsed(error => HandleError(stringWriter, error));
        }

        private void HandleError(StringWriter stringWriter, IEnumerable<Error> error)
        {
            if (error.IsHelp())
            {
                _output.WriteLine(stringWriter.ToString());
            }
        }

        private void RunWithParsedArguments(Options obj)
        {

            //if (args[0] == "task")
            //{
            //    _todoList.Add(new TodoItem { Task = args[2], Date = DateTime.Parse(args[4]) });
            //}
        }
    }
}