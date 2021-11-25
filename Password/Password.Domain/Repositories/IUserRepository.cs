using Password.Domain.Models;

namespace Password.Domain.Repositories
{
    public interface IUserRepository
    {
        int UserCount { get; }
        void Add(User user);
        User GetByEmail(string email);
        User GetByUserName(string username);
    }
}