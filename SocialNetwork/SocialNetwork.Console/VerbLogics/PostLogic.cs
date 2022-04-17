using SocialNetwork.Application.Repositories;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain.Requests;

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
            var request = new CreateUserRequest { Name = userName };
            var user = _userRepository.CreateIfNotExists(request);

            var post = new CreatePostRequest { Content = options.Message, UserId = user.Id };
            _postRepository.Create(post);

            return 0;
        }
    }
}
