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

        public void Create(User user)
        {
            _applicationDbContext.Users.Add(user);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _applicationDbContext.Users
                .Include(x => x.Subscriptions);
        }

        public void Update(User user)
        {
            _applicationDbContext.Entry(user).State = EntityState.Modified;
            _applicationDbContext.SaveChanges();
        }

        public User? GetById(int id)
        {
            return _applicationDbContext.Users.SingleOrDefault(x => x.Id == id);
        }
    }
}