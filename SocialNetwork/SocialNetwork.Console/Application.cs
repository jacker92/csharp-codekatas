using System;

namespace SocialNetwork.Console
{
    public class Application
    {
        private readonly IOutput _output;

        public Application(IOutput output)
        {
            _output = output;
        }

        public void Run(string[] args)
        {
            _output.Write("Name is required!");
        }
    }
}
