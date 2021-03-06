using AutoFixture;
using AutoMapper;
using Moq;
using SocialNetwork.Application.Mappings;
using SocialNetwork.Application.Services;
using SocialNetwork.Console.VerbLogics;
using SocialNetwork.Domain.DTO.Requests;
using SocialNetwork.Domain.DTO.Responses;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SocialNetwork.Console.IntegrationTests
{
    public class ApplicationTests : IntegrationTestBase
    {
        private readonly Mock<IOutput> _output;
        private readonly VerbLogicRunner _verbLogicRunner;
        private readonly PostLogic _postLogic;
        private readonly TimelineLogic _timelineLogic;
        private readonly FollowLogic _followLogic;
        private readonly WallLogic _wallLogic;
        private readonly ViewMessagesLogic _viewMessagesLogic;
        private readonly SendMessageLogic _sendMessagesLogic;
        private readonly Application _application;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly PostService _postService;
        private readonly DirectMessageService _directMessageService;
        private readonly SubscriptionService _subscriptionService;

        private CreateUserResponse _testUser1;
        private CreateUserResponse _testUser2;

        public ApplicationTests()
        {
            _output = new Mock<IOutput>();
            _mapper = MapperFactory.Create();

            _postService = new PostService(_postRepository, _userRepository, _mapper);
            _userService = new UserService(_userRepository, _mapper);
            _directMessageService = new DirectMessageService(_directMessageRepository, _userRepository, _mapper);
            _subscriptionService = new SubscriptionService(_subscriptionRepository, _userRepository);
            _timelineLogic = new TimelineLogic(_output.Object, _postService, _userService);
            _postLogic = new PostLogic(_output.Object, _postService, _userService);
            _followLogic = new FollowLogic(_userService, _subscriptionService, _output.Object);
            _wallLogic = new WallLogic(_userService, _postService, _output.Object);
            _viewMessagesLogic = new ViewMessagesLogic(_output.Object, _directMessageService, _userService);
            _sendMessagesLogic = new SendMessageLogic(_directMessageService, _userService, _output.Object);
            _verbLogicRunner = new VerbLogicRunner(_postLogic, _timelineLogic, _followLogic, _wallLogic, _viewMessagesLogic, _sendMessagesLogic);
            _application = new Application(_output.Object, _verbLogicRunner);

            InitializeTestUsers();
        }

        private void InitializeTestUsers()
        {
            var fixture = new Fixture();
            _testUser1 = _userService.CreateIfNotExists(fixture.Create<CreateUserRequest>());
            _testUser2 = _userService.CreateIfNotExists(fixture.Create<CreateUserRequest>());
        }

        [Theory, AutoMoqData]
        public void Run_Post_ShouldCreatePostMessage_WhichIsVisibleOnTimeline(string postContent)
        {
            _application.Run(new string[] { _testUser1.Name, "/post", postContent });
            _application.Run(new string[] { _testUser1.Name, "/timeline", _testUser1.Name });

            _output.Verify(x => x.WriteLine(postContent));
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
            AddPostsForUser(posts, _testUser2.Name);

            _application.Run(new string[] { _testUser1.Name, "/timeline", _testUser2.Name });

            _output.Verify(x => x.WriteLine($"{_testUser2.Name}'s timeline:"));

            foreach (var post in posts)
            {
                _output.Verify(x => x.WriteLine(post.Content));
            }
        }

        [Theory, AutoMoqData]
        public void Run_FollowUser_ShouldAddUserToSubscriptionList(IEnumerable<CreatePostRequest> posts)
        {
            AddPostsForUser(posts, _testUser2.Name);

            _application.Run(new string[] { _testUser1.Name, "/follow", _testUser2.Name });
            _application.Run(new string[] { _testUser1.Name, "/wall" });

            _output.Verify(x => x.WriteLine($"Subscribed to user's {_testUser2.Name} timeline."));
            foreach (var post in posts)
            {
                _output.Verify(x => x.WriteLine(post.Content));
            }
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
            AddPostsForUser(postRequests, _testUser2.Name);

            _application.Run(new string[] { _testUser1.Name, "/follow", _testUser2.Name });

            var posts = _postService.GetByUserId(_testUser2.Id).OrderByDescending(x => x.Created).ToList();

            int callOrder = 0;
            _output.Setup(x => x.WriteLine(posts[0].Content)).Callback(() => Assert.Equal(0, callOrder++));
            _output.Setup(x => x.WriteLine(posts[1].Content)).Callback(() => Assert.Equal(1, callOrder++));
            _output.Setup(x => x.WriteLine(posts[2].Content)).Callback(() => Assert.Equal(2, callOrder++));

            _application.Run(new string[] { _testUser1.Name, "/wall" });

            _output.Verify(x => x.WriteLine($"Showing {_testUser1.Name}'s wall:"));
            Assert.Equal(3, callOrder);
        }

        [Theory, AutoMoqData]
        public void Run_Wall_ShouldShowBothUsersPostsThatUserIsFollowing(IEnumerable<CreatePostRequest> postRequests, IEnumerable<CreatePostRequest> anotherPostRequests)
        {
            var testUser3 = "test";
            AddPostsForUser(postRequests, _testUser2.Name);
            AddPostsForUser(anotherPostRequests, testUser3);

            _application.Run(new string[] { _testUser1.Name, "/follow", _testUser2.Name });
            _application.Run(new string[] { _testUser1.Name, "/follow", testUser3 });

            _application.Run(new string[] { _testUser1.Name, "/wall" });

            _output.Verify(x => x.WriteLine($"Showing {_testUser1.Name}'s wall:"));

            foreach (var item in postRequests)
            {
                _output.Verify(x => x.WriteLine(item.Content));
            }

            foreach (var item in anotherPostRequests)
            {
                _output.Verify(x => x.WriteLine(item.Content));
            }
        }

        [Theory, AutoMoqData]
        public void Run_SendMessage_ShouldSendMessage(string content)
        {
            _application.Run(new string[] { _testUser1.Name, "/send_message", _testUser2.Name, content });
            _application.Run(new string[] { _testUser1.Name, "/view_messages" });

            _output.Verify(x => x.WriteLine($"Message sent to {_testUser2.Name}!"));
            _output.Verify(x => x.WriteLine($"{_testUser1.Name}: {content}"));
        }

        [Fact]
        public void Run_ViewMessages_ShouldShowNoMessages_IfNoMessagesArePresent()
        {
            _application.Run(new string[] { _testUser1.Name, "/view_messages" });

            _output.Verify(x => x.WriteLine($"No direct messages found."));
        }

        [Fact]
        public void Run_ViewMessages_ShouldShowNoMessages_IfNoMessagesArePresent_ForNewUser()
        {
            _application.Run(new string[] { "asdf", "/view_messages" });

            _output.Verify(x => x.WriteLine($"No direct messages found."));
        }

        [Theory, AutoMoqData]
        public void Run_ViewMessages_ShouldShowOneSentMessage_AfterMessageIsSent(string content)
        {
            _application.Run(new string[] { _testUser1.Name, "/send_message", _testUser2.Name, content });
            _application.Run(new string[] { _testUser1.Name, "/view_messages" });

            _output.Verify(x => x.WriteLine($"{_testUser1.Name}: " + content));
        }

        [Theory, AutoMoqData]
        public void Run_ViewMessages_ShouldShowOneReceivedMessage_AfterMessageIsSent(string content)
        {
            _application.Run(new string[] { _testUser1.Name, "/send_message", _testUser2.Name, content });
            _application.Run(new string[] { _testUser2.Name, "/view_messages" });

            _output.Verify(x => x.WriteLine($"{_testUser1.Name}: " + content));
        }

        private void AddPostsForUser(IEnumerable<CreatePostRequest> postRequests, string userName)
        {
            foreach (var postRequest in postRequests)
            {
                _application.Run(new string[] { userName, "/post", postRequest.Content });
            }
        }
    }
}