using SocialNetwork.Domain.DTO.Requests;

namespace SocialNetwork.Application.Services
{
    public interface ISubscriptionService
    {
        void Create(CreateSubscriptionRequest createSubscriptionRequest);
    }
}