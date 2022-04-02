using CommandLine;
using System.Text;
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

            ParseArgumentsAndInvoke(args);
        }

        private void ParseArgumentsAndInvoke(string[] args)
        {
            var stringWriter = new StringWriter();
            var parser = new Parser(config => config.HelpWriter = stringWriter);
            var arguments = parser.ParseArguments<AddOptions, GetAllOptions>(args)
                .MapResult(
                (AddOptions options) => RunAdd(options),
                (GetAllOptions options) => RunGetAll(options),
                errors => HandleError(stringWriter, errors));
        }

        private int RunGetAll(GetAllOptions options)
        {
            var items = _todoList.GetAll();
            var builder = new StringBuilder();
            foreach (var item in items)
            {
                builder.AppendLine($"Id: {item.Id}");
                builder.AppendLine($"Task: {item.Task}");
                builder.AppendLine($"Due: {item.Date.ToString("dd-MM-yyyy")}");
            }

            _output.WriteLine(builder.ToString());

            return (int)ApplicationExitCode.Ok;
        }

        private int HandleError(StringWriter stringWriter, IEnumerable<Error> error)
        {
            if (error.IsHelp())
            {
                _output.WriteLine(stringWriter.ToString());
            }

            return (int)ApplicationExitCode.Error;
        }

        private int RunAdd(AddOptions obj)
        {
            _todoList.Add(new TodoItem { Task = obj.TaskName, Date = obj.DueDate });

            return (int)ApplicationExitCode.Ok;
        }
    }
}