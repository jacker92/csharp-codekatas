using System;
using System.Linq;

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
            var name = args.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(name))
            {
                _output.Write("Name is required!");
            }

            _output.Write("Invalid verb is given!");
        }
    }
}
