using Moq;
using SocialNetwork.Application.Repositories;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Console.VerbLogics;
using SocialNetwork.Domain;
using Xunit;

namespace SocialNetwork.Console.Tests
{
    public class PostLogicTests
    {
        private readonly Mock<IOutput> _output;
        private readonly Mock<IPostRepository> _postRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly PostLogic _postLogic;

        public PostLogicTests()
        {
            _output = new Mock<IOutput>();
            _postRepository = new Mock<IPostRepository>();
            _userRepository = new Mock<IUserRepository>();
            _postLogic = new PostLogic(_output.Object, _postRepository.Object, _userRepository.Object);
        }

        [Theory, AutoMoqData]
        public void Run_ShouldPostMessageToUserTimeline(PostOptions postOptions, User user)
        {
            _userRepository.Setup(x => x.CreateIfNotExists(user.Name))
                .Returns(user);

            _postLogic.Run(postOptions, user.Name);

            _postRepository.Verify(x => x.Create(It.Is<Post>(x =>
            x.Content == postOptions.Message &&
            x.User.Name == user.Name)));

            _userRepository.Verify(x => x.CreateIfNotExists(user.Name));
        }
    }
}
