using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Repositories
{
    public interface IPostRepository
    {
        Post Create(Post post);
        IEnumerable<Post> GetByUserId(int userId);
        IEnumerable<Post> GetAll();
    }
}