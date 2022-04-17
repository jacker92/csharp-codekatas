namespace SocialNetwork.Domain.DTO.Responses
{
    public class CreateUserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Subscriptions { get; set; }
    }
}
