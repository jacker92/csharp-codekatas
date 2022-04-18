using SocialNetwork.Application.Repositories;
using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public void Create(CreateSubscriptionRequest createSubscriptionRequest)
        {
            var subscription = CreateSubscription(createSubscriptionRequest);
            _subscriptionRepository.Create(subscription);
            _subscriptionRepository.Save();
        }

        private Subscription CreateSubscription(CreateSubscriptionRequest createSubscriptionRequest)
        {
            return new Subscription
            {
                SubscriberId = createSubscriptionRequest.SubscriberId,
                SubscribedId = createSubscriptionRequest.SubscribedId
            };
        }
    }
}
