using TodoList.Console.CommandLineOptions;
using TodoList.Domain;

namespace TodoList.Console.VerbLogics
{
    public class AddVerbLogic : IAddVerbLogic
    {
        private readonly ITodoList _todoList;

        public AddVerbLogic(ITodoList todoList)
        {
            _todoList = todoList;
        }

        public int Run(AddOptions options)
        {
            _todoList.Add(new TodoItem { Task = options.TaskName, Date = options.DueDate });

            return (int)ApplicationExitCode.Ok;
        }
    }
}