using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;

namespace SocialNetwork.Application.Services
{
    public interface IUserService
    {
        CreateUserResponse CreateIfNotExists(CreateUserRequest createUserRequest);
    }
}