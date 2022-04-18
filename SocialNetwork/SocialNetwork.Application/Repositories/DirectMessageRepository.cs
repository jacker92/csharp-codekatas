using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure;
using System.Linq.Expressions;

namespace SocialNetwork.Application.Repositories
{
    public class DirectMessageRepository : BaseRepository<DirectMessage>, IDirectMessageRepository
    {
        public DirectMessageRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        { }
        public override IEnumerable<DirectMessage> GetWhere(Expression<Func<DirectMessage, bool>> predicate)
        {
            return _applicationDbContext.DirectMessages
                    .Where(predicate)
                    .Include(x => x.From)
                    .Include(x => x.To);
        }

    }
}