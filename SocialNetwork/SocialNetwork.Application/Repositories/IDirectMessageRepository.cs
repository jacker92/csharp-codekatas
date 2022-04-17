using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Repositories
{
    public interface IDirectMessageRepository
    {
        DirectMessage Create(DirectMessage directMessage);
        IEnumerable<DirectMessage> GetAll();
    }
}