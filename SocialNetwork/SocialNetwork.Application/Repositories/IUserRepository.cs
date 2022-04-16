using SocialNetwork.Domain;
using SocialNetwork.Domain.Requests;
using SocialNetwork.Domain.Responses;

namespace SocialNetwork.Application.Repositories
{
    public interface IUserRepository
    {
        CreateUserResponse CreateIfNotExists(CreateUserRequest createUserRequest);
        UpdateUserResponse Update(UpdateUserRequest request);
    }
}