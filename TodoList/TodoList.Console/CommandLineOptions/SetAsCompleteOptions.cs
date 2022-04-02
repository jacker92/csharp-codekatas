using CommandLine;

namespace TodoList.Console
{
    [Verb("complete", isDefault: true, HelpText = "Set task status as complete")]
    public class SetAsCompleteOptions
    {

    }
}