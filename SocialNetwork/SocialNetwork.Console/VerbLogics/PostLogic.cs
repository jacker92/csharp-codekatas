using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain;

namespace SocialNetwork.Console.VerbLogics
{
    public class PostLogic : IVerbLogic<PostOptions>
    {
        private readonly IOutput _output;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostLogic(IOutput output, IPostRepository postRepository, IUserRepository userRepository)
        {
            _output = output;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public int Run(PostOptions options, string userName)
        {
            var user = new User() { Name = userName };
            _userRepository.CreateIfNotExists(user);

            var post = new Post { Content = options.Message, User = user };
            _postRepository.Create(post);

            return 0;
        }
    }
}
