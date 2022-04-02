using System.Text;
using TodoList.Console.CommandLineOptions;
using TodoList.Domain;

namespace TodoList.Console.VerbLogics
{
    public class GetAllLogic : VerbLogic<GetAllOptions>
    {
        private readonly IOutput _output;
        private readonly ITodoList _todoList;

        public GetAllLogic(IOutput output, ITodoList todoList)
        {
            _output = output;
            _todoList = todoList;
        }

        public override int Run(GetAllOptions options)
        {
            var items = GetItems(options);
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

        private IEnumerable<TodoItem> GetItems(GetAllOptions options)
        {
            if (options.Status == TodoItemConsoleStatus.All)
            {
                return _todoList.GetAll();
            }

            return _todoList.GetAll(TodoItemStatus.Incomplete);
        }
    }
}