using CommandLine;

namespace TodoList.Console.CommandLineOptions
{
    [Verb("task", HelpText = "Add task to todo list.")]
    public class AddOptions
    {
        [Option('t', "name", Required = true, HelpText = "Task name")]
        public string TaskName { get; set; }

        [Option('d', "date", Required = false, HelpText = "Task due date")]
        public DateTime DueDate { get; set; }
    }
}