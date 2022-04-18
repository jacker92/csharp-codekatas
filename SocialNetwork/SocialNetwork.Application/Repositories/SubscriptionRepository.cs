using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure;

namespace SocialNetwork.Application.Repositories
{
    public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}