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

        [Theory, AutoMoqData]
        public void Run_ShouldReturnErrorMessage_IfInvalidVerbIsGiven(User user)
        {
            _application.Run(new string[] { user.Name, "/test" });

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
        public void Run_Timeline_ShouldBeEmpty_ByDefault(User invokedByUser, User userToView)
        {
            _application.Run(new string[] { invokedByUser.Name, "/timeline", userToView.Name });

            _output.Verify(x => x.WriteLine($"{userToView.Name}'s timeline does not contain any posts."));
        }

        [Theory, AutoMoqData]
        public void Run_PostMessage_ShouldBeVisible_OnUsersTimeline(IEnumerable<string> posts, string invokedBy, string toView)
        {
            var invokedByUser = _userRepository.CreateIfNotExists(invokedBy);
            var userToView = _userRepository.CreateIfNotExists(toView);

            AddPostsForUser(posts, userToView);

            _application.Run(new string[] { invokedBy, "/timeline", toView });

            _output.Verify(x => x.WriteLine($"{toView}'s timeline:"));

            foreach (var post in posts)
            {
                _output.Verify(x => x.WriteLine(post));
            }
        }

        [Theory, AutoMoqData]
        public void Run_FollowUser_ShouldAddUserToSubscriptionList(string invokedBy, string toView)
        {
            var invokedByUser = _userRepository.CreateIfNotExists(invokedBy);
            var userToView = _userRepository.CreateIfNotExists(toView);

            _application.Run(new string[] { invokedBy, "/follow", toView });

            var updatedUser = _userRepository.GetByName(invokedBy);

            _output.Verify(x => x.WriteLine($"Subscribed to user's {toView} timeline."));
            Assert.Single(updatedUser.Subscriptions);
            Assert.Equal(toView, updatedUser.Subscriptions[0].Name);
        }

        [Theory, AutoMoqData]
        public void Run_Wall_ShouldBeEmpty_ByDefault(string invokedBy)
        {
            var invokedByUser = _userRepository.CreateIfNotExists(invokedBy);

            _application.Run(new string[] { invokedBy, "/wall" });

            _output.Verify(x => x.WriteLine($"{invokedBy} has not yet subscribed to any user's posts."));
        }

        [Theory, AutoMoqData]
        public void Run_Wall_ShouldShowAllUsersPostThatUserHasSubscribed(string invokedBy, string toView, IEnumerable<string> posts)
        {
            var invokedByUser = _userRepository.CreateIfNotExists(invokedBy);
            var userToView = _userRepository.CreateIfNotExists(toView);
    
            AddPostsForUser(posts, userToView);

            invokedByUser.Subscriptions.Add(userToView);

            _application.Run(new string[] { invokedBy, "/wall" });

            _output.Verify(x => x.WriteLine($"Showing {invokedBy}'s wall:"));

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

        private void CreateUser(string user)
        {
            _userRepository.CreateIfNotExists(user);
        }
    }
}