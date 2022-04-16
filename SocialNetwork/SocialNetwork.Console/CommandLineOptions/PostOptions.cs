using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Console.CommandLineOptions
{
    [Verb("/post", HelpText = "Post message to personal timeline")]
    public class PostOptions
    {
        [Value(0, MetaName = "message", Required = true, HelpText = "Message")]
        public string Message { get; set; }
    }
}

