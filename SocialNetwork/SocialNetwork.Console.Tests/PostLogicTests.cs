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
        private readonly Mock<IPostRepository> _postsRepository;
        private readonly PostLogic _postLogic;

        public PostLogicTests()
        {
            _output = new Mock<IOutput>();
            _postsRepository = new Mock<IPostRepository>();
            _postLogic = new PostLogic(_output.Object, _postsRepository.Object);
        }

        [Fact]
        public void Run_ShouldPostMessageToUserTimeline()
        {
            var message = "hello";
            var userName = "user";
            _postLogic.Run(new CommandLineOptions.PostOptions { Message = message }, userName);
            _postsRepository.Verify(x => x.Save(It.Is<Post>(x =>
            x.Content == message &&
            x.User.Name == userName)));
        }
    }
}
