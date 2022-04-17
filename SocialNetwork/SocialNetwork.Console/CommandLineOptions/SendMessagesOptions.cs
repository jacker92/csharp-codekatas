using CommandLine;

namespace SocialNetwork.Console.CommandLineOptions
{
    [Verb("/send_message", HelpText = "View direct messages")]
    public class SendMessagesOptions
    {
        [Value(0, MetaName = "usertosend", Required = true, HelpText = "User to send message to")]
        public string UserToSend { get; set; }
    }
}

