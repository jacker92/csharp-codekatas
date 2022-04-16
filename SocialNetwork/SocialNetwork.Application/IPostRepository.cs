using SocialNetwork.Domain;

namespace SocialNetwork.Application
{
    public interface IPostRepository
    {
        void Create(Post post);
        IEnumerable<Post> GetPosts(User user);
    }
}