using System;
using System.Diagnostics.CodeAnalysis;

namespace HeavyMetalBakeSale.Console
{
    [ExcludeFromCodeCoverage]
    public class Display : IDisplay
    {
        public string AskInput()
        {
            return System.Console.ReadLine();
        }

        public void ShowOutput(string output)
        {
            if (output is null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            System.Console.Write(output);
        }
    }

}
