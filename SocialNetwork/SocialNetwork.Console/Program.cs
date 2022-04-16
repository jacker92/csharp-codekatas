using SocialNetwork.Application;
using SocialNetwork.Console.VerbLogics;
using SocialNetwork.Domain;
using SocialNetwork.Infrastructure;
using System;

namespace SocialNetwork.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var consoleOutput = new ConsoleOutput();
            var dbContext = new ApplicationDbContext();
            var postsRepository = new PostRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            var verbLogicRunner = new VerbLogicRunner(new PostLogic(consoleOutput, postsRepository, userRepository), new TimelineLogic(consoleOutput, postsRepository, userRepository));
            var application = new Application(consoleOutput, verbLogicRunner);

            application.Run(args);
        }
    }
}
