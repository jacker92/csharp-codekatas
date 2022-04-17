using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;

namespace SocialNetwork.Application.Services
{
    public interface IDirectMessageService
    {
        CreateDirectMessageResponse Create(CreateDirectMessageRequest request);
        IEnumerable<GetDirectMessageResponse> GetBySender(int fromId);
    }
}