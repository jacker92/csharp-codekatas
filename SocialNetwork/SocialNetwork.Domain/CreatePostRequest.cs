namespace SocialNetwork.Domain
{
    public class CreatePostRequest
    {
        public string Content { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
    }
}