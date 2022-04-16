using SocialNetwork.Console.CommandLineOptions;

namespace SocialNetwork.Console.VerbLogics
{
    public class PostLogic : IVerbLogic<PostOptions>
    {
        private readonly IOutput _output;

        public PostLogic(IOutput output)
        {
            _output = output;
        }

        public int Run(PostOptions options)
        {
            _output.WriteLine($"Running Post logic with message: {options.Message}");
            return 0;
        }
    }
}
