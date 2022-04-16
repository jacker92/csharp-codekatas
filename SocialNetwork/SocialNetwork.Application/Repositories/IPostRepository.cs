using SocialNetwork.Domain;
using SocialNetwork.Domain.Requests;

namespace SocialNetwork.Application.Repositories
{
    public interface IPostRepository
    {
        void Create(CreatePostRequest createPostRequest);
        IEnumerable<Post> GetByUserName(string user);
        IEnumerable<Post> GetAll();
    }
}