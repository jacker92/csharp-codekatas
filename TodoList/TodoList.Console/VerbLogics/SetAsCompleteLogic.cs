using TodoList.Console.CommandLineOptions;
using TodoList.Domain;

namespace TodoList.Console.VerbLogics
{
    public class SetAsCompleteLogic : IVerbLogic<SetAsCompleteOptions>
    {
        private readonly ITodoList _todoList;

        public SetAsCompleteLogic(ITodoList todoList)
        {
            _todoList = todoList;
        }

        public int Run(SetAsCompleteOptions options)
        {
            _todoList.Complete(options.Id);

            return (int)ApplicationExitCode.Ok;
        }
    }
}