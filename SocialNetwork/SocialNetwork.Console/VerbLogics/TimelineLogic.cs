using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain;
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
            _userRepository.CreateIfNotExists(userName);
            var user =  _userRepository.CreateIfNotExists(options.UserName);
            var posts = _postRepository.GetPosts(user);

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
