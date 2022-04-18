using SocialNetwork.Application.Services;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain.DTO.Requests;

namespace SocialNetwork.Console.VerbLogics
{
    public class FollowLogic : IVerbLogic<FollowOptions>
    {
        private readonly IUserService _userService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IOutput _output;

        public FollowLogic(IUserService userService, ISubscriptionService subscriptionService, IOutput output)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
            _output = output;
        }

        public int Run(FollowOptions options, string userName)
        {
            var user = _userService.CreateIfNotExists(new CreateUserRequest { Name = userName });
            var userToFollow = _userService.CreateIfNotExists(new CreateUserRequest { Name = options.UserToFollow });

            var createSubscriptionRequest = new CreateSubscriptionRequest
            {
                SubscriberId = user.Id,
                SubscribedId = userToFollow.Id
            };

            _subscriptionService.Create(createSubscriptionRequest);

            _output.WriteLine($"Subscribed to user's {options.UserToFollow} timeline.");

            return 0;
        }
    }
}
