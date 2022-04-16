using SocialNetwork.Domain;
using SocialNetwork.Infrastructure;
using System.Text;
using System.Text.Json;

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
        }

        public IEnumerable<Post> GetPosts(User user)
        {
            throw new NotImplementedException();
        }
    }
}