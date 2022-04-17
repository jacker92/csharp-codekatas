using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure;
using System.Linq.Expressions;

namespace SocialNetwork.Application.Repositories
{
    public class DirectMessageRepository : BaseRepository<DirectMessage>, IDirectMessageRepository
    {
        public DirectMessageRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}