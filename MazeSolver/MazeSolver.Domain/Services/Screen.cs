using System;

namespace MazeSolver.Services
{
    public class Screen : IScreen
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public void WriteOutput(string output)
        {
            Console.WriteLine(output);
        }
    }
}