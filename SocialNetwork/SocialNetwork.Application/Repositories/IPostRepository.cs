using SocialNetwork.Domain.Requests;
using SocialNetwork.Domain.Responses;

namespace SocialNetwork.Application.Repositories
{
    public interface IPostRepository
    {
        void Create(CreatePostRequest createPostRequest);
        IEnumerable<GetPostResponse> GetByUserId(int userId);
        IEnumerable<GetPostResponse> GetAll();
    }
}