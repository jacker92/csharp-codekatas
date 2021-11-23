namespace Password.Domain
{
    public interface IUserRepository
    {
        int UserCount { get; }
        void Add(User user);
        User GetByUserName(string username);
    }
}