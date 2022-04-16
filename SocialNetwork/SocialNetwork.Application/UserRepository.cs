using SocialNetwork.Domain;
using SocialNetwork.Infrastructure;

namespace SocialNetwork.Application
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public User CreateIfNotExists(string userName)
        {
            return new User { Name = userName };
        }
    }
}