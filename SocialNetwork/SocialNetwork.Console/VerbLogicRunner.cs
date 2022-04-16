using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Console.VerbLogics;

namespace SocialNetwork.Console
{
    public class VerbLogicRunner : IVerbLogicRunner
    {
        private readonly IVerbLogic<PostOptions> _postLogic;
        private readonly IVerbLogic<TimelineOptions> _timelineLogic;
        private readonly IVerbLogic<FollowOptions> _followLogic;    

        public VerbLogicRunner(IVerbLogic<PostOptions> postLogic, IVerbLogic<TimelineOptions> timelineLogic, IVerbLogic<FollowOptions> followLogic)
        {
            _postLogic = postLogic;
            _timelineLogic = timelineLogic;
            _followLogic = followLogic;
        }

        public void Run(object options, string userName)
        {
            switch (options)
            {
                case PostOptions p:
                    _postLogic.Run(p, userName);
                    break;
                case TimelineOptions t:
                    _timelineLogic.Run(t, userName);
                    break;
                case FollowOptions f:
                    _followLogic.Run(f, userName);
                    break;
            }
        }
    }
}
