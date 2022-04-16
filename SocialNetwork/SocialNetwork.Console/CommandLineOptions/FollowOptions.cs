using CommandLine;

namespace SocialNetwork.Console.CommandLineOptions
{
    [Verb("/follow", HelpText = "Follow user")]
    public class FollowOptions
    {
        [Value(0, MetaName = "usertofollow", Required = true, HelpText = "User to follow")]
        public string UserToFollow { get; set; }
    }
}

