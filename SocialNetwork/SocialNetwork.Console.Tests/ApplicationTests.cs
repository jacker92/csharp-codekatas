using AutoFixture;
using AutoMapper;
using Moq;
using SocialNetwork.Application;
using SocialNetwork.Application.Repositories;
using SocialNetwork.Console.VerbLogics;
using SocialNetwork.Domain;
using SocialNetwork.Domain.Requests;
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
        private readonly IMapper _mapper;

        private User _testUser1;
        private User _testUser2;

        public ApplicationTests()
        {
            _output = new Mock<IOutput>();
            _mapper = new MapperFactory().Create();
            var context = new AppDbContextFactory().CreateInMemoryDbContext();
            _postRepository = new PostRepository(context, _mapper);
            _userRepository = new UserRepository(context, _mapper);
            _timelineLogic = new TimelineLogic(_output.Object, _postRepository, _userRepository);
            _postLogic = new PostLogic(_output.Object, _postRepository, _userRepository);
            _followLogic = new FollowLogic(_userRepository, _output.Object);
            _wallLogic = new WallLogic(_userRepository, _postRepository, _output.Object);
            _verbLogicRunner = new VerbLogicRunner(_postLogic, _timelineLogic, _followLogic, _wallLogic);
            _application = new Application(_output.Object, _verbLogicRunner);

            InitializeTestUsers();
        }

        private void InitializeTestUsers()
        {
            var fixture = new Fixture();
            _testUser1 = _userRepository.CreateIfNotExists(fixture.Create<CreateUserRequest>());
            _testUser2 = _userRepository.CreateIfNotExists(fixture.Create<CreateUserRequest>());
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
        public void Run_Post_ShouldCreatePostMessage(string postContent)
        {
            _application.Run(new string[] { _testUser1.Name, "/post", postContent });

            var result = _postRepository.GetByUserName(_testUser1.Name);

            Assert.Single(result);
            Assert.Equal(result.Single().Content, postContent);
            Assert.Equal(result.Single().UserId, _testUser1.Id);
        }

        [Fact]
        public void Run_Post_WithMention_ShouldBeVisibleAtWall()
        {
            _application.Run(new string[] { _testUser1.Name, "/post", $"Hello @{_testUser2.Name}" });
            _application.Run(new string[] { _testUser2.Name, "/wall" });

            _output.Verify(x => x.WriteLine($"Hello @{_testUser2.Name}"));
        }

        [Fact]
        public void Run_Timeline_ShouldBeEmpty_ByDefault()
        {
            _application.Run(new string[] { _testUser1.Name, "/timeline", _testUser2.Name });

            _output.Verify(x => x.WriteLine($"{_testUser2.Name}'s timeline does not contain any posts."));
        }

        [Theory, AutoMoqData]
        public void Run_PostMessage_ShouldBeVisible_OnUsersTimeline(IEnumerable<CreatePostRequest> posts)
        {
            AddPostsForUser(posts, _testUser2);

            _application.Run(new string[] { _testUser1.Name, "/timeline", _testUser2.Name });

            _output.Verify(x => x.WriteLine($"{_testUser2.Name}'s timeline:"));

            foreach (var post in posts)
            {
                _output.Verify(x => x.WriteLine(post.Content));
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
        public void Run_Wall_ShouldShowAllUsersPostInOrderByCreationDate(IEnumerable<CreatePostRequest> postRequests)
        {
            AddPostsForUser(postRequests, _testUser2);
            _testUser1.Subscriptions.Add(_testUser2);

            var posts = _postRepository.GetByUserName(_testUser2.Name).OrderByDescending(x => x.Created).ToList();

            int callOrder = 0;
            _output.Setup(x => x.WriteLine(posts[0].Content)).Callback(() => Assert.Equal(0, callOrder++));
            _output.Setup(x => x.WriteLine(posts[1].Content)).Callback(() => Assert.Equal(1, callOrder++));
            _output.Setup(x => x.WriteLine(posts[2].Content)).Callback(() => Assert.Equal(2, callOrder++));

            _application.Run(new string[] { _testUser1.Name, "/wall" });

            _output.Verify(x => x.WriteLine($"Showing {_testUser1.Name}'s wall:"));
        }

        [Theory, AutoMoqData]
        public void Run_ShouldWriteError_IfApplicationThrowsError(string message)
        {
            var verbLogicRunner = new Mock<IVerbLogicRunner>();
            verbLogicRunner.Setup(x => x.Run(It.IsAny<object>(), It.IsAny<string>()))
                .Throws(new Exception(message));

            var application = new Application(_output.Object, verbLogicRunner.Object);

            application.Run(new string[] { _testUser1.Name, "/post", message });

            _output.Verify(x => x.WriteError(message));
        }

        [Fact]
        public void Run_ShouldWriteHelp_IfHelpIsRequested()
        {
            _application.Run(new string[] { _testUser1.Name, "/post", "--help" });

            _output.Verify(x => x.WriteLine(It.Is<string>(x => x.Contains("--help              Display this help screen."))));
        }

        private void AddPostsForUser(IEnumerable<CreatePostRequest> postRequests, User user)
        {
            foreach (var postRequest in postRequests)
            {
                postRequest.User = user;
            }

            _postRepository.Create(postRequests);
        }
    }
}