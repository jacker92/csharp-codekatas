using SocialNetwork.Domain;
using SocialNetwork.Domain.Requests;
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

        public void Create(IEnumerable<CreatePostRequest> posts)
        {
            var postsList = posts.Select(x => new Post { User = x.User, Content = x.Content, Created = x.Created });
            _applicationDbContext.Posts.AddRange(postsList);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Post> GetByUserName(string user)
        {
            return _applicationDbContext.Posts.Where(x => x.User.Name == user).ToList();
        }
    }
}