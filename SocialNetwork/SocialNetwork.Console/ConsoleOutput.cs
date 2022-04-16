using System.Diagnostics.CodeAnalysis;

namespace SocialNetwork.Console
{
    [ExcludeFromCodeCoverage]
    public class ConsoleOutput : IOutput
    {
        public void WriteLine(string message)
        {
            System.Console.WriteLine(message);
        }

        public void WriteError(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}
