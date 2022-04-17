using SocialNetwork.Application.Repositories;
using SocialNetwork.Application.Services;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain.DTO.Requests;

namespace SocialNetwork.Console.VerbLogics
{
    public class PostLogic : IVerbLogic<PostOptions>
    {
        private readonly IOutput _output;
        private readonly IPostRepository _postRepository;
        private readonly IUserService _userService;

        public PostLogic(IOutput output, IPostRepository postRepository, IUserService userService)
        {
            _output = output;
            _postRepository = postRepository;
            _userService = userService;
        }

        public int Run(PostOptions options, string userName)
        {
            var request = new CreateUserRequest { Name = userName };
            var user = _userService.CreateIfNotExists(request);

            var post = new CreatePostRequest { Content = options.Message, UserId = user.Id };
            _postRepository.Create(post);

            return 0;
        }
    }
}
