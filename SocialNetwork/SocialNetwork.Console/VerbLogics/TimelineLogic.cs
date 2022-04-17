using SocialNetwork.Application.Services;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain.DTO.Requests;
using System.Linq;

namespace SocialNetwork.Console.VerbLogics
{
    public class TimelineLogic : IVerbLogic<TimelineOptions>
    {
        private readonly IOutput _output;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public TimelineLogic(IOutput output, IPostService postService, IUserService userService)
        {
            _output = output;
            _postService = postService;
            _userService = userService;
        }

        public int Run(TimelineOptions options, string userName)
        {
            var request = new CreateUserRequest { Name = userName };
            var invokedByUser = _userService.CreateIfNotExists(request);

            request = new CreateUserRequest { Name = options.UserName };
            var userToView = _userService.CreateIfNotExists(request);

            var posts = _postService.GetByUserId(userToView.Id);

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
