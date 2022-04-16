using SocialNetwork.Domain;
using SocialNetwork.Infrastructure;

namespace SocialNetwork.Application.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public PostRepository(IApplicationDbContext dbContext)
        {
            _applicationDbContext = dbContext;
        }

        public void Create(Post post)
        {
            _applicationDbContext.Posts.Add(post);
            _applicationDbContext.SaveChanges();
        }

        public void Create(IEnumerable<Post> posts)
        {
            _applicationDbContext.Posts.AddRange(posts);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Post> GetByUserName(string user)
        {
            return _applicationDbContext.Posts.Where(x => x.User.Name == user).ToList();
        }
    }
}