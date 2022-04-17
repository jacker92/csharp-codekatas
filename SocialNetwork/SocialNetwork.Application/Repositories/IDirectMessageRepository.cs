using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;

namespace SocialNetwork.Application.Repositories
{
    public interface IDirectMessageRepository
    {
        CreateDirectMessageResponse Create(CreateDirectMessageRequest request);
    }
}