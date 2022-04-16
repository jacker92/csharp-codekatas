using SocialNetwork.Domain;
using SocialNetwork.Infrastructure;

namespace SocialNetwork.Application
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PostRepository(ApplicationDbContext dbContext)
        {
            _applicationDbContext = dbContext;
        }

        public void Create(Post post)
        {
            _applicationDbContext.Posts.Add(post);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Post> GetPosts(User user)
        {
            return _applicationDbContext.Posts.ToList();
        }
    }
}