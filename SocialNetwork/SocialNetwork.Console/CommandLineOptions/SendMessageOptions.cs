using CommandLine;

namespace SocialNetwork.Console.CommandLineOptions
{
    [Verb("/send_message", HelpText = "View direct messages")]
    public class SendMessageOptions
    {
        [Value(0, MetaName = "usertosend", Required = true, HelpText = "User to send message to")]
        public string UserToSend { get; set; }

        [Value(1, MetaName = "content", Required = true, HelpText = "Message content")]
        public string Content { get; set; }
    }
}

