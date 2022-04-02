using System.Text;
using TodoList.Console.CommandLineOptions;
using TodoList.Domain;

namespace TodoList.Console.VerbLogics
{
    public class GetAllLogic : IVerbLogic<GetAllOptions>
    {
        private readonly IOutput _output;
        private readonly ITodoList _todoList;

        public GetAllLogic(IOutput output, ITodoList todoList)
        {
            _output = output;
            _todoList = todoList;
        }

        public int Run(GetAllOptions options)
        {
            var items = GetItems(options);
            var groupByParent = items.GroupBy(x => x.ParentId);
            var builder = new StringBuilder();
            foreach (var item in groupByParent.Where(x => x.Key != null))
            {
                var parent = items.SingleOrDefault(x => x.Id == item.Key);

                AddValues(builder, parent);
                foreach (var child in item.ToList())
                {
                    builder.AppendLine("> Child Task <");
                    AddValues(builder, child);
                }

                builder.AppendLine();
            }

            foreach (var item in groupByParent.Where(x => x.Key == null))
            {
                foreach (var i in item.ToList())
                {
                    if (!groupByParent.Any(x => x.Key == i.Id))
                    {
                        AddValues(builder, i);
                    }
                }
            }

            _output.WriteLine(builder.ToString());

            return (int)ApplicationExitCode.Ok;
        }

        private static void AddValues(StringBuilder builder, TodoItem? item)
        {
            builder.AppendLine($"Id: {item.Id}");
            builder.AppendLine($"Task: {item.Task}");
            builder.AppendLine($"Due: {item.Date.ToString("dd-MM-yyyy")}");
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