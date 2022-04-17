namespace SocialNetwork.Domain.Responses
{
    public class GetPostResponse
    {
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public int UserId { get; set; }
    }
}
