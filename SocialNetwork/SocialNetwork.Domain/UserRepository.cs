namespace SocialNetwork.Domain
{
    public class UserRepository : IUserRepository
    {
        public User CreateIfNotExists(string userName)
        {
            return new User { Name = userName };
        }
    }
}