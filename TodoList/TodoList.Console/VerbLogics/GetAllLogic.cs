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
            foreach (var item in groupByParent.OrderByDescending(x => x.Key))
            {
                if (item.Key == null)
                {
                    foreach (var result in item.ToList()
                        .Where(i => !groupByParent.Any(x => x.Key == i.Id))
                        .Select(x => AddValues(x)))
                    {
                        builder.Append(result);
                    }

                    continue;
                }

                var parent = items.Single(x => x.Id == item.Key);

                builder.Append(AddValues(parent));
                foreach (var child in item.ToList())
                {
                    builder.AppendLine("> Child Task <");
                    builder.Append(AddValues(child));
                }

                builder.AppendLine();
            }

            _output.WriteLine(builder.ToString());

            return (int)ApplicationExitCode.Ok;
        }

        private static string AddValues(TodoItem item)
        {
            return
@$"Id: {item.Id}
Task: {item.Task}
Due: {item.Date.ToString("dd-MM-yyyy")}
";
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