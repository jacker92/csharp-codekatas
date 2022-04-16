using SocialNetwork.Domain;
using SocialNetwork.Domain.Requests;

namespace SocialNetwork.Application.Repositories
{
    public interface IUserRepository
    {
        User CreateIfNotExists(CreateUserRequest createUserRequest);
        User Update(User user);
    }
}