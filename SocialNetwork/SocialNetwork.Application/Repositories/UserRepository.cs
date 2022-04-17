using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Models;
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

        public User Create(User user)
        {
            var result = _applicationDbContext.Users.Add(user);
            _applicationDbContext.SaveChanges();
            return result.Entity;
        }

        public IEnumerable<User> GetAll()
        {
            return _applicationDbContext.Users
                .Include(x => x.Subscriptions);
        }

        public User Update(User user)
        {
            var result = _applicationDbContext.Users.Update(user);
            _applicationDbContext.SaveChanges();

            return result.Entity;
        }

        public User? GetById(int id)
        {
            return _applicationDbContext.Users.SingleOrDefault(x => x.Id == id);
        }
    }
}