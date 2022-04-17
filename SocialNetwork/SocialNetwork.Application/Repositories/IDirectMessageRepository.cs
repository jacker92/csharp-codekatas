using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Repositories
{
    public interface IDirectMessageRepository
    {
        void Create(DirectMessage directMessage);
        IEnumerable<DirectMessage> GetAll();
        void Save();
    }
}