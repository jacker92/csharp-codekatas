using CommandLine;

namespace TodoList.Console.CommandLineOptions
{
    [Verb("complete", isDefault: true, HelpText = "Set task status as complete")]
    public class SetAsCompleteOptions
    {

    }
}