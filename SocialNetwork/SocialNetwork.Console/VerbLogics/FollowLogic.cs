using SocialNetwork.Application.Repositories;
using SocialNetwork.Application.Services;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain.DTO.Requests;

namespace SocialNetwork.Console.VerbLogics
{
    public class FollowLogic : IVerbLogic<FollowOptions>
    {
        private readonly IUserService _userService;
        private readonly IOutput _output;

        public FollowLogic(IUserService userService, IOutput output)
        {
            _userService = userService;
            _output = output;
        }

        public int Run(FollowOptions options, string userName)
        {
            var user = _userService.CreateIfNotExists(new CreateUserRequest { Name = userName });
            var userToFollow = _userService.CreateIfNotExists(new CreateUserRequest { Name = options.UserToFollow });

            var updateUserRequest = new UpdateUserRequest
            {
                Id = user.Id,
                Name = user.Name,
                Subscriptions = user.Subscriptions
            };

            updateUserRequest.Subscriptions.Add(userToFollow.Id);

            _userService.Update(updateUserRequest);

            _output.WriteLine($"Subscribed to user's {options.UserToFollow} timeline.");

            return 0;
        }
    }
}
