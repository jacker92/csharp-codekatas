using CommandLine;
using TodoList.Domain;

namespace TodoList.Console
{
    [Verb("task", HelpText = "Add task to todo list.")]
    public class AddOptions
    {
        [Option('t', "name", Required = true, HelpText = "Task name")]
        public string TaskName { get; set; }

        [Option('d', "date", Required = false, HelpText = "Task due date")]
        public DateTime DueDate { get; set; }
    }

    [Verb("list", HelpText = "List task specifying the status of the take.")]
    public class ListOptions
    {

    }

    [Verb("complete", isDefault: true, HelpText = "Set task status as complete")]
    public class SetAsCompleteOptions
    {

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

        private static ParserResult<AddOptions> ParseArguments(string[] args, StringWriter stringWriter)
        {
            var parser = new Parser(config => config.HelpWriter = stringWriter);
            var arguments = parser.ParseArguments<AddOptions>(args);
            return arguments;
        }

        private void Run(StringWriter stringWriter, ParserResult<AddOptions> arguments)
        {
            arguments.WithParsed(options => RunAdd(options))
                     .WithNotParsed(error => HandleError(stringWriter, error));
        }

        private void HandleError(StringWriter stringWriter, IEnumerable<Error> error)
        {
            if (error.IsHelp())
            {
                _output.WriteLine(stringWriter.ToString());
            }
        }

        private void RunAdd(AddOptions obj)
        {
            _todoList.Add(new TodoItem { Task = obj.TaskName, Date = obj.DueDate });
        }
    }
}