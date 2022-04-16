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

        public void Create(Post post)
        {
            _applicationDbContext.Posts.Add(post);
            _applicationDbContext.SaveChanges();
        }

        public void Create(IEnumerable<CreatePostRequest> posts)
        {
            var postsList = _mapper.Map<IEnumerable<Post>>(posts);
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