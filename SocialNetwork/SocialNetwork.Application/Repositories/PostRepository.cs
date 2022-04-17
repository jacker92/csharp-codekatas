using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;
using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure;

namespace SocialNetwork.Application.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public PostRepository(IApplicationDbContext dbContext, IMapper mapper)
        {
            _applicationDbContext = dbContext;
            _mapper = mapper;
        }

        public void Create(CreatePostRequest createPostRequest)
        {
            var post = CreatePost(createPostRequest);

            _applicationDbContext.Posts.Add(post);
            _applicationDbContext.SaveChanges();
        }

        public void Create(IEnumerable<CreatePostRequest> createPostRequests)
        {
            var postsList = createPostRequests.Select(x => CreatePost(x));
            _applicationDbContext.Posts.AddRange(postsList);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<GetPostResponse> GetAll()
        {
            var posts = _applicationDbContext.Posts
                .AsNoTracking();

            return _mapper.Map<IEnumerable<GetPostResponse>>(posts);
        }

        public IEnumerable<GetPostResponse> GetByUserId(int userId)
        {
            var posts = _applicationDbContext.Posts
               .Include(x => x.User)
               .Where(x => x.User.Id == userId)
               .AsNoTracking();

            return _mapper.Map<IEnumerable<GetPostResponse>>(posts);
        }

        private Post CreatePost(CreatePostRequest createPostRequest)
        {
            var user = _applicationDbContext.Users.Single(x => x.Id == createPostRequest.UserId);

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