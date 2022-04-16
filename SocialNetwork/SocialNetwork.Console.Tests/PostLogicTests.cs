using AutoFixture;
using Moq;
using SocialNetwork.Console.CommandLineOptions;
using SocialNetwork.Console.VerbLogics;
using SocialNetwork.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SocialNetwork.Console.Tests
{
    public class PostLogicTests
    {
        private readonly Mock<IOutput> _output;
        private readonly Mock<IPostRepository> _postRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly PostLogic _postLogic;
        private readonly Fixture _fixture;

        public PostLogicTests()
        {
            _output = new Mock<IOutput>();
            _postRepository = new Mock<IPostRepository>();
            _userRepository = new Mock<IUserRepository>();
            _postLogic = new PostLogic(_output.Object, _postRepository.Object, _userRepository.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public void Run_ShouldPostMessageToUserTimeline()
        {
            var postOptions = _fixture.Create<PostOptions>();
            var userName = _fixture.Create<string>();

            _userRepository.Setup(x => x.CreateIfNotExists(userName))
                .Returns(new User { Name = userName });

            _postLogic.Run(postOptions, userName);

            _postRepository.Verify(x => x.Create(It.Is<Post>(x =>
            x.Content == postOptions.Message &&
            x.User.Name == userName)));

            _userRepository.Verify(x => x.CreateIfNotExists(userName));
        }
    }
}
