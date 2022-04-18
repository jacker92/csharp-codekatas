namespace SocialNetwork.Domain.DTO.Requests
{
    public class CreateSubscriptionRequest
    {
        public int SubscriberId { get; set; }
        public int SubscribedId { get; set; }
    }
}