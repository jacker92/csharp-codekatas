﻿using SocialNetwork.Application.Repositories;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Console.VerbLogics
{
    public class WallLogic : IVerbLogic<WallOptions>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IOutput _output;

        public WallLogic(IUserRepository userRepository, IPostRepository postRepository, IOutput output)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _output = output;
        }

        public int Run(WallOptions options, string userName)
        {
            var user = _userRepository.CreateIfNotExists(userName);

            if (!user.Subscriptions.Any())
            {
                _output.WriteLine($"{userName} has not yet subscribed to any user's posts.");
                return 0;
            }

            _output.WriteLine($"Showing {userName}'s wall:");
            var posts = new List<Post>();
            foreach (var sub in user.Subscriptions)
            {
                var subscribedPosts = _postRepository.GetByUserName(sub.Name);
                posts.AddRange(subscribedPosts);
            }

            foreach (var post in posts.OrderByDescending(x => x.Created))
            {
                _output.WriteLine(post.Content);
            }

            return 1;
        }
    }
}
