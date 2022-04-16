using Microsoft.EntityFrameworkCore;
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
            var existing = GetByName(userName);

            if (existing != null)
            {
                return existing;
            }

            var user = new User { Name = userName };

            var result = _applicationDbContext.Users.Add(user);
            _applicationDbContext.SaveChanges();
            return result.Entity;
        }

        public User Update(User user)
        {
            var result = _applicationDbContext.Users.Update(user);
            _applicationDbContext.SaveChanges();
            return result.Entity;
        }

        public User GetByName(string name)
        {
            return _applicationDbContext.Users
                .Include(x => x.Subscriptions)
                .SingleOrDefault(x => x.Name == name)!;
        }
    }
}