namespace SocialNetwork.Domain
{
    public interface IPostRepository
    {
        void Create(Post post);
        IEnumerable<Post> GetPosts(User user);
    }
}