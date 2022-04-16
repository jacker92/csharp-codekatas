using SocialNetwork.Application.Repositories;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain.Requests;
using System.Linq;

namespace SocialNetwork.Console.VerbLogics
{
    public class TimelineLogic : IVerbLogic<TimelineOptions>
    {
        private readonly IOutput _output;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public TimelineLogic(IOutput output, IPostRepository postRepository, IUserRepository userRepository)
        {
            _output = output;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public int Run(TimelineOptions options, string userName)
        {
            var request = new CreateUserRequest { Name = userName };
            _userRepository.CreateIfNotExists(request);

            request = new CreateUserRequest { Name = options.UserName };
            _userRepository.CreateIfNotExists(request);

            var posts = _postRepository.GetByUserName(options.UserName);

            if (!posts.Any())
            {
                _output.WriteLine($"{options.UserName}'s timeline does not contain any posts.");
                return 0;
            }

            _output.WriteLine($"{options.UserName}'s timeline:");
            foreach (var post in posts)
            {
                _output.WriteLine(post.Content);
            }

            return 0;
        }
    }
}
