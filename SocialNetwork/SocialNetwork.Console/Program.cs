using SocialNetwork.Console.VerbLogics;
using System;

namespace SocialNetwork.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var consoleOutput = new ConsoleOutput();
            var verbLogicRunner = new VerbLogicRunner(new PostLogic(consoleOutput));
            var application = new Application(consoleOutput, verbLogicRunner);

            application.Run(args);
        }
    }
}
