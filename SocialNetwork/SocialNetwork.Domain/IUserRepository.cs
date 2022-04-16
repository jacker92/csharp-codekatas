namespace SocialNetwork.Domain
{
    public interface IUserRepository
    {
        void CreateIfNotExists(User user);
    }
}