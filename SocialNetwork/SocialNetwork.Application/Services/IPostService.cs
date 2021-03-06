using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;

namespace SocialNetwork.Application.Services
{
    public interface IPostService
    {
        void Create(CreatePostRequest createPostRequest);
        IEnumerable<GetPostResponse> GetByUserId(int userId);
        IEnumerable<GetPostResponse> GetWhereMentioned(string userName);
    }
}