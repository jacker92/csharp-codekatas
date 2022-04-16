using SocialNetwork.Domain;
using SocialNetwork.Infrastructure;

namespace SocialNetwork.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UserRepository(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public User CreateIfNotExists(string userName)
        {
            return new User { Name = userName };
        }

        public User Update(User user)
        {
            var result = _applicationDbContext.Users.Update(user);
            _applicationDbContext.SaveChanges();
            return result.Entity;
        }
    }
}