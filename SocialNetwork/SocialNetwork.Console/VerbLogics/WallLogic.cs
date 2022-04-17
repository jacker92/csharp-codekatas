using SocialNetwork.Application.Services;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Console.VerbLogics
{
    public class WallLogic : IVerbLogic<WallOptions>
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly IOutput _output;

        public WallLogic(IUserService userService, IPostService postService, IOutput output)
        {
            _userService = userService;
            _postService = postService;
            _output = output;
        }

        public int Run(WallOptions options, string userName)
        {
            var request = new CreateUserRequest { Name = userName };
            var user = _userService.CreateIfNotExists(request);
            var mentions = _postService.GetAll().Where(x => x.Content.Contains($"@{userName}"));

            if (!user.Subscriptions.Any() && !mentions.Any())
            {
                _output.WriteLine($"{userName} has not yet subscribed to any user's posts.");
                return 0;
            }

            _output.WriteLine($"Showing {userName}'s wall:");
            var posts = new List<GetPostResponse>();
            foreach (var sub in user.Subscriptions)
            {
                var subscribedPosts = _postService.GetByUserId(sub);
                posts.AddRange(subscribedPosts);
            }

            foreach (var mention in mentions)
            {
                if (!posts.Contains(mention))
                {
                    posts.Add(mention);
                }
            }

            foreach (var post in posts.OrderByDescending(x => x.Created))
            {
                _output.WriteLine(post.Content);
            }

            return 1;
        }
    }
}
