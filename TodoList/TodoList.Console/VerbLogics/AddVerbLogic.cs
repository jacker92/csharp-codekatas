using TodoList.Domain;

namespace TodoList.Console
{
    public class AddVerbLogic : VerbLogic<AddOptions>
    {
        private readonly ITodoList _todoList;

        public AddVerbLogic(ITodoList todoList)
        {
            _todoList = todoList;
        }

        public override int Run(AddOptions options)
        {
            _todoList.Add(new TodoItem { Task = options.TaskName, Date = options.DueDate });

            return (int)ApplicationExitCode.Ok;
        }
    }
}