using System;

namespace SocialNetwork.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var consoleOutput = new ConsoleOutput();
            var application = new Application(consoleOutput);

            application.Run(args);
        }
    }
}
