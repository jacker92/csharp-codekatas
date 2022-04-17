using SocialNetwork.Domain.DTO.Responses;

namespace SocialNetwork.Domain.DTO.Requests
{
    public class CreateDirectMessageRequest
    {
        public int From { get; set; }
        public int To { get; set; }
        public string Content { get; set; }
    }
}