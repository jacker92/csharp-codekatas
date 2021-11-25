using Password.Domain.Models;

namespace Password.Domain.Repositories
{
    public interface IUserRepository
    {
        int UserCount { get; }
        void Add(User user);
        User GetByUserName(string username);
    }
}