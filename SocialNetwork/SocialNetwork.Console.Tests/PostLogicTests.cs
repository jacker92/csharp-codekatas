using Moq;
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

        [Fact]
        public void Run_ShouldPostMessageToUserTimeline()
        {
            var message = "hello";
            var userName = "user";
            _postLogic.Run(new CommandLineOptions.PostOptions { Message = message }, userName);
            _postRepository.Verify(x => x.Save(It.Is<Post>(x =>
            x.Content == message &&
            x.User.Name == userName)));

            _userRepository.Verify(x => x.Save(It.Is<User>(x => x.Name == userName)));
        }
    }
}
