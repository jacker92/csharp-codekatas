using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure;
using System.Linq.Expressions;

namespace SocialNetwork.Application.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override IEnumerable<Post> GetWhere(Expression<Func<Post, bool>> predicate)
        {
            return _applicationDbContext.Posts
                .Where(predicate)
                .Include(x => x.User);
        }
    }
}