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

        public void Create(Post post)
        {
            _applicationDbContext.Posts.Add(post);
        }

        public IEnumerable<Post> GetAll()
        {
            return _applicationDbContext.Posts
                .Include(x => x.User);
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}