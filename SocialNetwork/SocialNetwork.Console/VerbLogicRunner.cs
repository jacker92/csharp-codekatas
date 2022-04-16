using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Console.VerbLogics;

namespace SocialNetwork.Console
{
    public class VerbLogicRunner : IVerbLogicRunner
    {
        private readonly IVerbLogic<PostOptions> _postLogic;

        public VerbLogicRunner(IVerbLogic<PostOptions> postLogic)
        {
            _postLogic = postLogic;
        }

        public void Run(object options, string userName)
        {
            switch (options)
            {
                case PostOptions p:
                    _postLogic.Run(p, userName);
                    break;
            }
        }
    }
}
