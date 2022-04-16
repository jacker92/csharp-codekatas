using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain;

namespace SocialNetwork.Console.VerbLogics
{
    public class PostLogic : IVerbLogic<PostOptions>
    {
        private readonly IOutput _output;
        private readonly IPostsRepository _postsRepository;

        public PostLogic(IOutput output, IPostsRepository postsRepository)
        {
            _output = output;
            _postsRepository = postsRepository;
        }

        public int Run(PostOptions options, string userName)
        {
            var post = new Post { Content = options.Message, User = new User() { Name = userName } };
            _postsRepository.Save(post);
            return 0;
        }
    }
}
