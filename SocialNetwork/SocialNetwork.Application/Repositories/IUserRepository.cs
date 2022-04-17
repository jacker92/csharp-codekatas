using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Repositories
{
    public interface IUserRepository
    {
        User Create(User user);
        User Update(User user);
        IEnumerable<User> GetAll();
        User? GetById(int id);
    }
}