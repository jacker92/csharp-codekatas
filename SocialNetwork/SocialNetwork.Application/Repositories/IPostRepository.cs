using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;

namespace SocialNetwork.Application.Repositories
{
    public interface IPostRepository
    {
        void Create(CreatePostRequest createPostRequest);
        IEnumerable<GetPostResponse> GetByUserId(int userId);
        IEnumerable<GetPostResponse> GetAll();
    }
}