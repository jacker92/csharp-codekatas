using Moq;
using SocialNetwork.Console.VerbLogics;
using SocialNetwork.Domain;
using System;
using Xunit;

namespace SocialNetwork.Console.Tests
{
    public class ApplicationTests
    {
        private readonly Mock<IOutput> _output;
        private readonly VerbLogicRunner _verbLogicRunner;
        private readonly PostLogic _postLogic;
        private readonly Mock<IPostRepository> _postRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly TimelineLogic _timelineLogic;
        private readonly Application _application;

        public ApplicationTests()
        {
            _output = new Mock<IOutput>();
            _postRepository = new Mock<IPostRepository>();
            _userRepository = new Mock<IUserRepository>();
            _timelineLogic = new TimelineLogic(_output.Object);
            _postLogic = new PostLogic(_output.Object, _postRepository.Object, _userRepository.Object);
            _verbLogicRunner = new VerbLogicRunner(_postLogic, _timelineLogic);
            _application = new Application(_output.Object, _verbLogicRunner);
        }

        [Fact]
        public void Run_ShouldReturnErrorMessage_IfNullArgumentListIsGiven()
        {
            _application.Run(null);

            _output.Verify(x => x.WriteError("No arguments were given!"));
        }

        [Fact]
        public void Run_ShouldReturnErrorMessage_IfNoNameIsGiven()
        {
            _application.Run(Array.Empty<string>());

            _output.Verify(x => x.WriteError("Name is required!"));
        }

        [Fact]
        public void Run_ShouldReturnErrorMessage_IfNameIsWhitespace()
        {
            _application.Run(new string[] {" "});

            _output.Verify(x => x.WriteError("Name is required!"));
        }

        [Fact]
        public void Run_ShouldReturnErrorMessage_IfInvalidVerbIsGiven()
        {
            _application.Run(new string[] { "Alice", "/test" });

            _output.Verify(x => x.WriteError(It.Is<string>(x => x.Contains("Verb '/test' is not recognized."))));
        }

        [Fact]
        public void Run_Timeline_ShouldBeEmpty_ByDefault()
        {
            _application.Run(new string[] { "Bob", "/timeline", "Alice" });

            _output.Verify(x => x.WriteLine("Alice's timeline does not contain any posts."));
        }

        //[Fact]
        //public void Run_PostMessage_ShouldBeVisible_OnUsersTimeline()
        //{
        //    CreatePost();

        //    _application.Run(new string[] { "Bob", "/timeline", "Alice" });

        //    _output.Verify(x => x.WriteLine("Alice's timeline:"));
        //    _output.Verify(x => x.WriteLine("What a wonderfully sunny day!"));
        //}

        private void CreatePost()
        {
            _application.Run(new string[] { "Alice", "/post", "What a wonderfully sunny day!" });
            _output.Reset();
        }
    }
}