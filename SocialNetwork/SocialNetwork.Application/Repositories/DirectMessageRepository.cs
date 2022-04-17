using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure;

namespace SocialNetwork.Application.Repositories
{
    public class DirectMessageRepository : IDirectMessageRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DirectMessageRepository(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public DirectMessage Create(DirectMessage directMessage)
        {
            var created = _applicationDbContext.DirectMessages.Add(directMessage);
            _applicationDbContext.SaveChanges();

            return created.Entity;
        }

        public IEnumerable<DirectMessage> GetAll()
        {
            return _applicationDbContext.DirectMessages;
        }
    }
}