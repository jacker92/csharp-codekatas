using SocialNetwork.Console.VerbLogics;
using SocialNetwork.Domain;
using System;

namespace SocialNetwork.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var consoleOutput = new ConsoleOutput();
            var postsRepository = new PostRepository();
            var verbLogicRunner = new VerbLogicRunner(new PostLogic(consoleOutput, postsRepository));
            var application = new Application(consoleOutput, verbLogicRunner);

            application.Run(args);
        }
    }
}
