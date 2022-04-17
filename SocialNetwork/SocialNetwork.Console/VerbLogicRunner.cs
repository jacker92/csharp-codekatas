using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Console.VerbLogics;

namespace SocialNetwork.Console
{
    public class VerbLogicRunner : IVerbLogicRunner
    {
        private readonly IVerbLogic<PostOptions> _postLogic;
        private readonly IVerbLogic<TimelineOptions> _timelineLogic;
        private readonly IVerbLogic<FollowOptions> _followLogic;
        private readonly IVerbLogic<WallOptions> _wallLogic;
        private readonly IVerbLogic<ViewMessagesOptions> _viewMessagesLogic;
        private readonly IVerbLogic<SendMessageOptions> _sendMessagesLogic;

        public VerbLogicRunner(IVerbLogic<PostOptions> postLogic,
                               IVerbLogic<TimelineOptions> timelineLogic,
                               IVerbLogic<FollowOptions> followLogic,
                               IVerbLogic<WallOptions> wallLogic,
                               IVerbLogic<ViewMessagesOptions> viewMessagesLogic,
                               IVerbLogic<SendMessageOptions> sendMessagesLogic)
        {
            _postLogic = postLogic;
            _timelineLogic = timelineLogic;
            _followLogic = followLogic;
            _wallLogic = wallLogic;
            _viewMessagesLogic = viewMessagesLogic;
            _sendMessagesLogic = sendMessagesLogic;
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
                case WallOptions w:
                    _wallLogic.Run(w, userName);
                    break;
                case ViewMessagesOptions v:
                    _viewMessagesLogic.Run(v, userName);
                    break;
                case SendMessageOptions s:
                    _sendMessagesLogic.Run(s, userName);
                    break;
            }
        }
    }
}
