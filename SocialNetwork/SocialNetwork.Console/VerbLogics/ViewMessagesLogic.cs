using SocialNetwork.Console.CommandLineOptions;

namespace SocialNetwork.Console.VerbLogics
{
    public class ViewMessagesLogic : IVerbLogic<ViewMessagesOptions>
    {
        private readonly IOutput _output;

        public ViewMessagesLogic(IOutput output)
        {
            _output = output;
        }

        public int Run(ViewMessagesOptions options, string userName)
        {
            _output.WriteLine("No direct messages found.");
            return 0;
        }
    }
}
