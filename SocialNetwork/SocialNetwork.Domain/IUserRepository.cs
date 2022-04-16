namespace SocialNetwork.Domain
{
    public interface IUserRepository
    {
        User CreateIfNotExists(string userName);
    }
}