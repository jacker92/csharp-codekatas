using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Repositories
{
    public interface IPostRepository
    {
        void Create(Post post);
        IEnumerable<Post> GetAll();
        void Save();
    }
}