namespace SocialNetwork.Domain.Requests
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Subscriptions { get; set; }
    }
}