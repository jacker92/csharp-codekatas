namespace SocialNetwork.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Subscriptions { get; set; }
    }
}