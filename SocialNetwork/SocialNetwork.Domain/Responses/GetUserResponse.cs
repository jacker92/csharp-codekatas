using System.Collections;

namespace SocialNetwork.Domain.Responses
{
    public class GetUserResponse
    {
        public IEnumerable<GetUserResponse> Subscriptions { get; set; }
        public int Id { get; set; }
    }
}
