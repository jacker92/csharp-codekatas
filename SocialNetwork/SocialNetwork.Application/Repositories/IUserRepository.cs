using SocialNetwork.Domain;

namespace SocialNetwork.Application.Repositories
{
    public interface IUserRepository
    {
        User CreateIfNotExists(string userName);
        User Update(User user);
    }
}