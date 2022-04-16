using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using SocialNetwork.Application;
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

        public PostLogicTests()
        {
            _output = new Mock<IOutput>();
            _postRepository = new Mock<IPostRepository>();
            _userRepository = new Mock<IUserRepository>();
            _postLogic = new PostLogic(_output.Object, _postRepository.Object, _userRepository.Object);
        }

        [Theory, AutoData]
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
