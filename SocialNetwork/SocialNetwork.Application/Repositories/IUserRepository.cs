using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;

namespace SocialNetwork.Application.Repositories
{
    public interface IUserRepository
    {
        CreateUserResponse CreateIfNotExists(CreateUserRequest createUserRequest);
        UpdateUserResponse Update(UpdateUserRequest request);
    }
}