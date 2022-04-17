using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Models;
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

        public Post Create(Post post)
        {
            var created = _applicationDbContext.Posts.Add(post);
            _applicationDbContext.SaveChanges();
            return created.Entity;
        }

        public void CreateMany(IEnumerable<Post> posts)
        {
            _applicationDbContext.Posts.AddRange(posts);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Post> GetAll()
        {
            return _applicationDbContext.Posts;
        }

        public IEnumerable<Post> GetByUserId(int userId)
        {
            return _applicationDbContext.Posts
               .Include(x => x.User)
               .Where(x => x.User.Id == userId)
               .AsNoTracking();
        }
    }
}