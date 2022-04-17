using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Repositories
{
    public interface IUserRepository
    {
        void Create(User user);
        void Update(User user);
        IEnumerable<User> GetAll();
        User? GetById(int id);
        void Save();
    }
}