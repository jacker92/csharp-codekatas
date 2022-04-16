using SocialNetwork.Console.VerbLogics;
using SocialNetwork.Domain;
using System;

namespace SocialNetwork.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var consoleOutput = new ConsoleOutput();
            var postsRepository = new PostRepository();
            var userRepository = new UserRepository();
            var verbLogicRunner = new VerbLogicRunner(new PostLogic(consoleOutput, postsRepository, userRepository), new TimelineLogic(consoleOutput, postsRepository, userRepository));
            var application = new Application(consoleOutput, verbLogicRunner);

            application.Run(args);
        }
    }
}
