using CommandLine;

namespace TodoList.Console
{
    [Verb("list", HelpText = "Lists tasks.")]
    public class GetAllOptions
    {
        [Option('s', "status", Default = TodoItemConsoleStatus.All, HelpText = "Task status")]
        public TodoItemConsoleStatus Status { get; set; }
    }
}