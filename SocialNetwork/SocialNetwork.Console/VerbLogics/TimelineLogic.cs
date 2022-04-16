using SocialNetwork.Console.CommandLineOptions;

namespace SocialNetwork.Console.VerbLogics
{
    public class TimelineLogic : IVerbLogic<TimelineOptions>
    {
        private readonly IOutput _output;

        public TimelineLogic(IOutput output)
        {
            _output = output;
        }

        public int Run(TimelineOptions options, string userName)
        {
            _output.WriteLine($"{options.UserName}'s timeline does not contain any posts.");
           // _output.WriteLine($"{userName}'s timeline:");

            return 0;
        }
    }
}
