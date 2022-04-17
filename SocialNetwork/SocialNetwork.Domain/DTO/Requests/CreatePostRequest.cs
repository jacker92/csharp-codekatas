namespace SocialNetwork.Domain.DTO.Requests
{
    public class CreatePostRequest
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
    }
}