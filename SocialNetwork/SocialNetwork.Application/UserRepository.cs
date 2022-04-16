using SocialNetwork.Domain;

namespace SocialNetwork.Application
{
    public class UserRepository : IUserRepository
    {
        public User CreateIfNotExists(string userName)
        {
            return new User { Name = userName };
        }
    }
}