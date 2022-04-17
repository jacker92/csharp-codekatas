namespace SocialNetwork.Domain.DTO.Responses
{
    public class GetUserResponse
    {
        public IEnumerable<GetUserResponse> Subscriptions { get; set; }
        public int Id { get; set; }
        public object Name { get; set; }
    }
}
