using AutoMapper;
using SocialNetwork.Application.Repositories;
using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void Create(CreatePostRequest createPostRequest)
        {
            var post = CreatePost(createPostRequest);
            _postRepository.Create(post);
            _postRepository.Save();
        }

        public IEnumerable<GetPostResponse> GetWhereMentioned(string userName)
        {
            var posts = _postRepository.GetWhere(x => x.Content.Contains($"@{userName}"));

            return _mapper.Map<IEnumerable<GetPostResponse>>(posts);
        }

        public IEnumerable<GetPostResponse> GetByUserId(int userId)
        {
            var posts = _postRepository.GetWhere(x => x.User.Id == userId);

            return _mapper.Map<IEnumerable<GetPostResponse>>(posts);
        }

        private Post CreatePost(CreatePostRequest createPostRequest)
        {
            var user = _userRepository.GetById(createPostRequest.UserId)!;

            var post = new Post
            {
                User = user,
                Content = createPostRequest.Content,
                Created = DateTime.UtcNow
            };
            return post;
        }
    }
}
