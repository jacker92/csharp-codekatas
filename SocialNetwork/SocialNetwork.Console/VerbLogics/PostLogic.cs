using SocialNetwork.Application.Services;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain.DTO.Requests;

namespace SocialNetwork.Console.VerbLogics
{
    public class PostLogic : IVerbLogic<PostOptions>
    {
        private readonly IOutput _output;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public PostLogic(IOutput output, IPostService postService, IUserService userService)
        {
            _output = output;
            _postService = postService;
            _userService = userService;
        }

        public int Run(PostOptions options, string userName)
        {
            var request = new CreateUserRequest { Name = userName };
            var user = _userService.CreateIfNotExists(request);

            var post = new CreatePostRequest { Content = options.Message, UserId = user.Id };
            _postService.Create(post);

            return 0;
        }
    }
}
