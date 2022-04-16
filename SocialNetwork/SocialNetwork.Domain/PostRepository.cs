namespace SocialNetwork.Domain
{
    public class PostRepository : IPostRepository
    {
        public void Create(Post post)
        {

        }

        public IEnumerable<Post> GetPosts(User user)
        {
            throw new NotImplementedException();
        }
    }
}