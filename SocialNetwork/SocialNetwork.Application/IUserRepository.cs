using SocialNetwork.Domain;

namespace SocialNetwork.Application
{
    public interface IUserRepository
    {
        User CreateIfNotExists(string userName);
        User Update(User user);
    }
}