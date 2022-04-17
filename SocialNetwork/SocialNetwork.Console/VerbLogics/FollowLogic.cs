using SocialNetwork.Application.Repositories;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain.DTO.Requests;

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
            var user = _userRepository.CreateIfNotExists(new CreateUserRequest { Name = userName });
            var userToFollow = _userRepository.CreateIfNotExists(new CreateUserRequest { Name = options.UserToFollow });

            var updateUserRequest = new UpdateUserRequest
            {
                Id = user.Id,
                Name = user.Name,
                Subscriptions = user.Subscriptions
            };

            updateUserRequest.Subscriptions.Add(userToFollow.Id);

            _userRepository.Update(updateUserRequest);

            _output.WriteLine($"Subscribed to user's {options.UserToFollow} timeline.");

            return 0;
        }
    }
}
