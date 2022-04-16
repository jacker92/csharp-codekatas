using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain;
using SocialNetwork.Domain.Requests;
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
            var post = _mapper.Map<Post>(createPostRequest);
            _applicationDbContext.Posts.Add(post);
            _applicationDbContext.SaveChanges();
        }

        public void Create(IEnumerable<CreatePostRequest> createPostRequests)
        {
            var postsList = _mapper.Map<IEnumerable<Post>>(createPostRequests);
            _applicationDbContext.Posts.AddRange(postsList);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Post> GetAll()
        {
            return _applicationDbContext.Posts
                .AsNoTracking();
        }

        public IEnumerable<Post> GetByUserName(string user)
        {
            return _applicationDbContext.Posts
                .Include(x => x.User)
                .Where(x => x.User.Name == user)
                .AsNoTracking();
        }
    }
}