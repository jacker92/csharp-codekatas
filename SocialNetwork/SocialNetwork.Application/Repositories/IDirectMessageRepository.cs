using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Repositories
{
    public interface IDirectMessageRepository
    {
        DirectMessage Create(DirectMessage directMessage);
        IEnumerable<DirectMessage> GetAll();
    }
}