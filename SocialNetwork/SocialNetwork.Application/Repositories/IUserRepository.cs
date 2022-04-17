using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;
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