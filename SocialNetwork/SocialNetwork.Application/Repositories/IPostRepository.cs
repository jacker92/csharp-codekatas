using SocialNetwork.Domain;

namespace SocialNetwork.Application.Repositories
{
    public interface IPostRepository
    {
        void Create(Post post);
        IEnumerable<Post> GetByUserName(string user);
    }
}