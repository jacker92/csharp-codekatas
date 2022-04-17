namespace SocialNetwork.Domain.DTO.Responses
{
    public class GetDirectMessageResponse
    {
        public GetUserResponse From { get; set; }
        public GetUserResponse To { get; set; }
        public string Content { get; set; }
    }
}
