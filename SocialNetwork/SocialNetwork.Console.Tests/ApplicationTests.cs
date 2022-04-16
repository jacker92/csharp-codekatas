using AutoFixture;
using Moq;
using SocialNetwork.Application.Repositories;
using SocialNetwork.Console.VerbLogics;
using SocialNetwork.Domain;
using SocialNetwork.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SocialNetwork.Console.Tests
{
    public class ApplicationTests
    {
        private readonly Mock<IOutput> _output;
        private readonly VerbLogicRunner _verbLogicRunner;
        private readonly PostLogic _postLogic;
        private readonly PostRepository _postRepository;
        private readonly UserRepository _userRepository;
        private readonly TimelineLogic _timelineLogic;
        private readonly FollowLogic _followLogic;
        private readonly WallLogic _wallLogic;
        private readonly Application _application;

        private readonly User _testUser1;
        private readonly User _testUser2;

        public ApplicationTests()
        {
            _output = new Mock<IOutput>();
            var context = new AppDbContextFactory().CreateInMemoryDbContext();
            _postRepository = new PostRepository(context);
            _userRepository = new UserRepository(context);
            _timelineLogic = new TimelineLogic(_output.Object, _postRepository, _userRepository);
            _postLogic = new PostLogic(_output.Object, _postRepository, _userRepository);
            _followLogic = new FollowLogic(_userRepository, _output.Object);
            _wallLogic = new WallLogic(_userRepository, _postRepository, _output.Object);
            _verbLogicRunner = new VerbLogicRunner(_postLogic, _timelineLogic, _followLogic, _wallLogic);
            _application = new Application(_output.Object, _verbLogicRunner);

            var fixture = new Fixture();
            _testUser1 = _userRepository.CreateIfNotExists(fixture.Create<string>());
            _testUser2 = _userRepository.CreateIfNotExists(fixture.Create<string>());
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
            _application.Run(new string[] { " " });

            _output.Verify(x => x.WriteError("Name is required!"));
        }

        [Fact]
        public void Run_ShouldReturnErrorMessage_IfInvalidVerbIsGiven()
        {
            _application.Run(new string[] { _testUser1.Name, "/test" });

            _output.Verify(x => x.WriteError(It.Is<string>(x => x.Contains("Verb '/test' is not recognized."))));
        }

        [Theory, AutoMoqData]
        public void Run_Post_ShouldCreatePostMessage(string postContent, string userToCreate)
        {
            var user = _userRepository.CreateIfNotExists(userToCreate);

            _application.Run(new string[] { userToCreate, "/post", postContent });

            var result = _postRepository.GetByUserName(userToCreate);

            Assert.Single(result);
            Assert.Equal(result.Single().Content, postContent);
            Assert.Equal(result.Single().User, user);
        }

        [Theory, AutoMoqData]
        public void Run_Timeline_ShouldBeEmpty_ByDefault(string invokedByUser, string userToView)
        {
            _application.Run(new string[] { invokedByUser, "/timeline", userToView });

            _output.Verify(x => x.WriteLine($"{userToView}'s timeline does not contain any posts."));
        }

        [Theory, AutoMoqData]
        public void Run_PostMessage_ShouldBeVisible_OnUsersTimeline(IEnumerable<string> posts)
        {
            AddPostsForUser(posts, _testUser2);

            _application.Run(new string[] { _testUser1.Name, "/timeline", _testUser2.Name });

            _output.Verify(x => x.WriteLine($"{_testUser2.Name}'s timeline:"));

            foreach (var post in posts)
            {
                _output.Verify(x => x.WriteLine(post));
            }
        }

        [Fact]
        public void Run_FollowUser_ShouldAddUserToSubscriptionList()
        {
            _application.Run(new string[] { _testUser1.Name, "/follow", _testUser2.Name });

            var updatedUser = _userRepository.GetByName(_testUser1.Name);

            _output.Verify(x => x.WriteLine($"Subscribed to user's {_testUser2.Name} timeline."));
            Assert.Single(updatedUser.Subscriptions);
            Assert.Equal(_testUser2.Name, updatedUser.Subscriptions[0].Name);
        }

        [Fact]
        public void Run_Wall_ShouldBeEmpty_ByDefault()
        {
            _application.Run(new string[] { _testUser1.Name, "/wall" });

            _output.Verify(x => x.WriteLine($"{_testUser1.Name} has not yet subscribed to any user's posts."));
        }

        [Theory, AutoMoqData]
        public void Run_Wall_ShouldShowAllUsersPostThatUserHasSubscribed(IEnumerable<string> posts)
        {
            AddPostsForUser(posts, _testUser2);

            _testUser1.Subscriptions.Add(_testUser2);

            _application.Run(new string[] { _testUser1.Name, "/wall" });

            _output.Verify(x => x.WriteLine($"Showing {_testUser1.Name}'s wall:"));

            foreach (var post in posts)
            {
                _output.Verify(x => x.WriteLine(post));
            }
        }

        [Theory, AutoMoqData]
        public void Run_ShouldWriteError_IfApplicationThrowsError(string userName, string message)
        {
            var verbLogicRunner = new Mock<IVerbLogicRunner>();
            verbLogicRunner.Setup(x => x.Run(It.IsAny<object>(), It.IsAny<string>()))
                .Throws(new Exception(message));

            var application = new Application(_output.Object, verbLogicRunner.Object);

            application.Run(new string[] { userName, "/post", message });

            _output.Verify(x => x.WriteError(message));
        }

        [Theory, AutoMoqData]
        public void Run_ShouldWriteHelp_IfHelpIsRequested(string userName)
        {
            _application.Run(new string[] { userName, "/post", "--help" });

            _output.Verify(x => x.WriteLine(It.Is<string>(x => x.Contains("--help              Display this help screen."))));
        }

        private void AddPostsForUser(IEnumerable<string> postMessages, User user)
        {
            var posts = postMessages.Select(x => new Post { Content = x, User = user });
            _postRepository.Create(posts);
        }
    }
}