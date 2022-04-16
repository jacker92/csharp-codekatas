using SocialNetwork.Application;
using SocialNetwork.Console.CommandLineOptions;

namespace SocialNetwork.Console.VerbLogics
{

    public class FollowLogic : IVerbLogic<FollowOptions>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOutput _output;

        public FollowLogic(IUserRepository userRepository, IOutput output)
        {
            _userRepository = userRepository;
            _output = output;
        }

        public int Run(FollowOptions options, string userName)
        {
            var user = _userRepository.CreateIfNotExists(userName);
            var userToFollow = _userRepository.CreateIfNotExists(options.UserToFollow);

            user.Subscriptions.Add(userToFollow);

            _userRepository.Update(user);

            _output.WriteLine($"Subscribed to user's {options.UserToFollow} timeline.");

            return 0;
        }
    }
}
