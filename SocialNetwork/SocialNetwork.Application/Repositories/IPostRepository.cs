using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Repositories
{
    public interface IPostRepository
    {
        Post Create(Post post);
        void CreateMany(IEnumerable<Post> posts);
        IEnumerable<Post> GetByUserId(int userId);
        IEnumerable<Post> GetAll();
    }
}