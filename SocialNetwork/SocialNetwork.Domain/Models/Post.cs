namespace SocialNetwork.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
    }
}