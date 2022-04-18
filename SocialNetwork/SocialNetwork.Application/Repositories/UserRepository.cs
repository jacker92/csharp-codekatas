using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure;
using System.Linq.Expressions;

namespace SocialNetwork.Application.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override IEnumerable<User> GetWhere(Expression<Func<User, bool>> predicate)
        {
            return _applicationDbContext.Users
                 .Where(predicate)
                 .Include(x => x.Subscriptions)
                 .ThenInclude(x => x.Subscribed);
        }
    }
}