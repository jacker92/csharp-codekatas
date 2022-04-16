using CommandLine;

namespace SocialNetwork.Console.CommandLineOptions
{
    [Verb("/timeline", HelpText = "View person's timeline")]
    public class TimelineOptions
    {
        [Value(0, MetaName = "username", Required = true, HelpText = "User's timeline to view")]
        public string UserName { get; set; }
    }
}

