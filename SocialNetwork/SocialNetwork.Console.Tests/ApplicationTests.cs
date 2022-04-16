using Moq;
using SocialNetwork.Application.Repositories;
using SocialNetwork.Console.VerbLogics;
using SocialNetwork.Domain;
using SocialNetwork.Infrastructure;
using System;
using System.Collections.Generic;
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
        private readonly FollowLogic _followLogic;
        private readonly WallLogic _wallLogic;
        private readonly Application _application;

        public ApplicationTests()
        {
            _output = new Mock<IOutput>();
            _postRepository = new Mock<IPostRepository>();
            _userRepository = new Mock<IUserRepository>();
            _timelineLogic = new TimelineLogic(_output.Object, _postRepository.Object, _userRepository.Object);
            _postLogic = new PostLogic(_output.Object, _postRepository.Object, _userRepository.Object);
            _followLogic = new FollowLogic(_userRepository.Object, _output.Object);
            _wallLogic = new WallLogic(_userRepository.Object, _postRepository.Object, _output.Object);
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
        public void Run_Post_ShouldCreatePostMessage(Post post, User user)
        {
            SetupUserRepositoryUser(user);

            _application.Run(new string[] { user.Name, "/post", post.Content });

            _postRepository.Verify(x => x.Create(It.Is<Post>(x => x.Content == post.Content && x.User.Name == user.Name)));
        }

        [Theory, AutoMoqData]
        public void Run_Timeline_ShouldBeEmpty_ByDefault(User invokedByUser, User userToView)
        {
            _application.Run(new string[] { invokedByUser.Name, "/timeline", userToView.Name });

            _output.Verify(x => x.WriteLine($"{userToView.Name}'s timeline does not contain any posts."));
        }

        [Theory, AutoMoqData]
        public void Run_PostMessage_ShouldBeVisible_OnUsersTimeline(IEnumerable<Post> posts, User invokedByUser, User userToView)
        {
            SetupUserRepositoryUser(invokedByUser);
            SetupUserRepositoryUser(userToView);
            SetupPostRepositoryPosts(posts, userToView);

            _application.Run(new string[] { invokedByUser.Name, "/timeline", userToView.Name });

            _output.Verify(x => x.WriteLine($"{userToView.Name}'s timeline:"));

            foreach (var post in posts)
            {
                _output.Verify(x => x.WriteLine(post.Content));
            }
        }

        [Theory, AutoMoqData]
        public void Run_FollowUser_ShouldAddUserToSubscriptionList(User invokedByUser, User userToView)
        {
            SetupUserRepositoryUser(invokedByUser);
            SetupUserRepositoryUser(userToView);

            _application.Run(new string[] { invokedByUser.Name, "/follow", userToView.Name });

            _output.Verify(x => x.WriteLine($"Subscribed to user's {userToView.Name} timeline."));

            _userRepository.Verify(x => x.Update(It.Is<User>(x => x == invokedByUser && x.Subscriptions.Contains(userToView))));
        }

        [Theory, AutoMoqData]
        public void Run_Wall_ShouldBeEmpty_ByDefault(User invokedByUser)
        {
            SetupUserRepositoryUser(invokedByUser);

            _application.Run(new string[] { invokedByUser.Name, "/wall" });

            _output.Verify(x => x.WriteLine($"{invokedByUser.Name} has not yet subscribed to any user's posts."));
        }

        [Theory, AutoMoqData]
        public void Run_Wall_ShouldShowAllUsersPostThatUserHasSubscribed(User invokedByUser, User userToView, IEnumerable<Post> posts)
        {
            invokedByUser.Subscriptions.Add(userToView);

            SetupUserRepositoryUser(invokedByUser);
            SetupUserRepositoryUser(userToView);
            SetupPostRepositoryPosts(posts, userToView);

            _application.Run(new string[] { invokedByUser.Name, "/wall" });

            _output.Verify(x => x.WriteLine($"Showing {invokedByUser.Name}'s wall:"));

            foreach (var post in posts)
            {
                _output.Verify(x => x.WriteLine(post.Content));
            }
        }

        [Theory, AutoMoqData]
        public void Run_ShouldWriteError_IfApplicationThrowsError(string userName, string message)
        {
            var verbLogicRunner = new Mock<IVerbLogicRunner>();
            verbLogicRunner.Setup(x => x.Run(It.IsAny<object>(), It.IsAny<string>()))
                .Throws(new Exception(message));

            var application = new Application(_output.Object, verbLogicRunner.Object);

            application.Run(new string[] { userName, "/post", message});

            _output.Verify(x => x.WriteError(message));
        }

        [Theory, AutoMoqData]
        public void Run_ShouldWriteHelp_IfHelpIsRequested(string userName)
        {
            _application.Run(new string[] { userName, "/post", "--help" });

            _output.Verify(x => x.WriteLine(It.Is<string>(x => x.Contains("--help              Display this help screen."))));
        }

        private void SetupPostRepositoryPosts(IEnumerable<Post> posts, User userToView)
        {
            _postRepository.Setup(x => x.GetPosts(userToView))
             .Returns(posts);
        }

        private void SetupUserRepositoryUser(User user)
        {
            _userRepository.Setup(x => x.CreateIfNotExists(user.Name))
                                        .Returns(user);
        }
    }
}