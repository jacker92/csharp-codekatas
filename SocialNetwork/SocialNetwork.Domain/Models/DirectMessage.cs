namespace SocialNetwork.Domain.Models
{
    public class DirectMessage
    {
        public int Id { get; set; }
        public User From { get; set; }
        public User To { get; set; }
        public string Content { get; set; }
    }
}