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

        public void Create(DirectMessage directMessage)
        {
            _applicationDbContext.DirectMessages.Add(directMessage);
        }

        public IEnumerable<DirectMessage> GetAll()
        {
            return _applicationDbContext.DirectMessages;
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}