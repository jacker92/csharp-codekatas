using SocialNetwork.Application.Repositories;
using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserRepository _userRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IUserRepository userRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
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
                Subscriber = _userRepository.GetById(createSubscriptionRequest.SubscriberId),
                Subscribed = _userRepository.GetById(createSubscriptionRequest.SubscribedId)
            };
        }
    }
}
